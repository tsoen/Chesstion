/**
 * Variables globales.
 */
var opensGlob; // Liste des Opens du tournoi.
var refGlob = -1; // Reference du tournoi affiché.
var prixVieux; // Coût d'inscription pour la categorie d'age superieure.
var prixJeune; // Coût d'inscription pour la categorie d'age inferieure.
var limiteAge; // Age separant les deux categories d'age.
var modeFFE = true; // Mode de remplissage du formulaire, avec ou sans NrFFE.

/**
 *  Initialise les variables globales.
 * @param opens (array) - Liste des Opens du tournoi.
 * @param ref (int) - Reference du tournoi affiché.
 * @param prixvieux (double) - Coût d'inscription pour la categorie d'age superieure.
 * @param prixjeune (double) - Coût d'inscription pour la categorie d'age inferieure.
 * @param limiteage (int) - Age separant les deux categories d'age.
 */
function initialise(opens, ref, prixvieux, prixjeune, limiteage){
	try {
		opensGlob = opens;
		refGlob = ref;
		prixVieux = prixvieux;
		prixJeune = prixjeune;
		limiteAge = limiteage;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Affichage des vues.
 */

/**
 * Envoit une requête pour afficher le contenu d'une page demandée et met à jour la barre de navigation.
 * @param page (string) - Nom de la page (fichier) à afficher.
 * @returns {boolean}
 */
function getOutput(page) {
    var url = window.location.pathname;
    var filePath = url.substring(0, url.lastIndexOf('/'));
    var file = filePath.substring(filePath.lastIndexOf('/')+1);

    if(file === page){
    	return true;
	}

    try {
		var navbar = document.getElementById('navbar');
		for (var i = 0; i < navbar.childElementCount; i++) {
			if (navbar.children[i].id === page)
				navbar.children[i].className = "nav-item active";
			else
				navbar.children[i].className = "nav-item";
		}

		getRequest(
			'../' + page + '/' + refGlob,
			drawOutput,
			drawError
		);
		return false;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Requête d'acces au contenu d'une page.
 * @param url (string) - URL de la page à afficher.
 * @param success - Traitement en cas de réussite de la requête.
 * @param error - Traitement en cas d'échec de la requête.
 * @returns {boolean}
 */
function getRequest(url, success, error) {
	var req = false;
	try{
		// most browsers
		req = new XMLHttpRequest();
	} catch (e){
		// IE
		try{
			req = new ActiveXObject("Msxml2.XMLHTTP");
		} catch(e) {
			// try an older version
			try{
				req = new ActiveXObject("Microsoft.XMLHTTP");
			} catch(e) {
				return false;
			}
		}
	}
	if (!req) return false;
	if (typeof success != 'function') success = function () {};
	if (typeof error != 'function') error = function () {};
	req.onreadystatechange = function(){
		if(req.readyState === 4) {
			return req.status === 200 ? success(req.responseText) : error(req.status);
		}
	};
	req.open("GET", url, true);
	req.send(null);
	return req;
}

/**
 * Reçoit le contenu de la page demandée et l'affiche.
 * @param responseText (string) - Contenu de la page.
 */
function drawOutput(responseText) {
	try {
		var container = document.getElementById('include');
		container.innerHTML = responseText;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Auto-complétion du formulaire
 */

/**
 *  Auto-complétion du formulaire, déclenche Joueur.getInfo(nrffe).
 * @param nrffe (string) - NrFFE du joueur.
 * @returns {boolean}
 */
function autoComp(nrffe){
	try {
		getRequest(
			refGlob + '?fonction=getInfo&NrFFE=' + nrffe,
			completeFields,
			drawError
		);
		return false;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Remplit les champs du formulaire.
 * @param responseText (string) - Informations renvoyées par Joueur.getInfo().
 */
function completeFields(responseText) {
	try {
		JSON.parse(responseText);
		message('success', responseText);
	}
	catch (Exception) {
		message('error', responseText);
	}

	try {
		var Infos = JSON.parse(responseText);

		var name = document.getElementById('name');
		name.value = Infos['Nom'];

		var firstName = document.getElementById('firstName');
		firstName.value = Infos['Prenom'];

		var club = document.getElementById('club');
		club.value = Infos['ClubRef'];

		var ligue = document.getElementById('ligue');
		ligue.value = Infos['Ligue'];

		var elo = document.getElementById('elo');
		elo.value = Infos['Elo'];
		disableOpens(Infos['Elo']);

		var birthdate = document.getElementById('birthdate');
		birthdate.value = Infos['NeLe'];
		setFrais();

		var sexe = document.getElementById('sexe');
		sexe.value = Infos['Sexe'];

		var country = document.getElementById('country');
		country.value = Infos['Federation'];
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Affiche un message d'erreur
 */
function drawError() {
	var container = document.getElementById('include');
	container.innerHTML = 'Bummer: there was an error!';
}

/**
 * Affiche un fichier généré par Papi dans un nouvel onglet.
 * USED IN RESULTATS.PHP
 * @param tr (string) - Nom du fichier.
 */
function papiHtml(tr){
	window.open('/~soen/Chesstion/html/' + refGlob + '/' + tr.value + '.html');
}

