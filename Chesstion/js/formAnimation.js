
/**
 * N'est plus utilisé - Affiche le champ pour numéro de téléphone.
 * @param checked (boolean) - Afficher ou cacher l'élément.
 */
function showPhone(checked) {
	setVisible(document.getElementById('alertDevices-phone'), checked)
}

/**
 * Petite animation pour afficher ou cacher un élément html.
 * @param element (élément html) - Element à afficher ou cacher.
 * @param visible (boolean) - Afficher ou cacher l'élément.
 */
function setVisible(element, visible) {
	if (visible) {
		$(element).slideDown('fast', 'swing');
		element.querySelector('input').setAttribute('required', '');
	} else {
		$(element).slideUp('fast', 'swing');
		element.querySelector('input').removeAttribute('required');
	}
}

/**
 * Désactive les champs en fonction du mode d'inscription : avec ou sans NrFFE.
 * @param reset (boolean) - true pour vider les champs du formulaire, false pour garder les valeurs actuelles.
 */
function switchMode(reset){
	try {
		if (reset === true) {
            resetFields(true);
        }

		var maform = document.forms[0];
		var champ;

		if (modeFFE === true) {

			for (champ = 1; champ < 10; champ++) {
				if (maform.elements[champ].disabled) {
					maform.elements[champ].disabled = false;
				}
			}
			document.getElementById('licence').disabled = true;
			document.getElementById('club').disabled = true;
			document.getElementById('ligue').disabled = true;

		}
		else {
			for (champ = 1; champ < 10; champ++) {
				if (!maform.elements[champ].disabled) {
					maform.elements[champ].disabled = true;
				}
			}
			document.getElementById('licence').disabled = false;
		}

		modeFFE = !modeFFE;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Vide les champs du formulaire.
 * @param all (boolean) - true pour vider tous les champs, false pour ne vider que les champs grisés.
 */
function resetFields(all){
	try {
		var maform = document.forms[0];
		var champ;

		if (all === true) {
			for (champ = 0; champ < maform.elements.length; champ++) {
				if (maform.elements[champ].name !== 'meal')
					maform.elements[champ].value = "";
			}
		}
		else {
			for (champ = 2; champ < 10; champ++) {
				maform.elements[champ].value = "";
			}
		}

		// Remet les frais à zéro.
		setFrais();
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Désactive les checkbox des Opens en fonction de l'elo du joueur.
 * @param elo (int) - Elo du joueur.
 */
function disableOpens(elo) {
	try {
		var type = 'open';

		for (var o = 0; o < (opensGlob.length); o++) {
			this[type + opensGlob[o]['Nom']] = document.getElementById('open-' + opensGlob[o]['Nom']);
			if (elo > opensGlob[o]['EloMax'] && opensGlob[o]['EloMax'] !== -1) {
				this[type + opensGlob[o]['Nom']].disabled = true;
				this[type + opensGlob[o]['Nom']].checked = false;
			}
			else {
				this[type + opensGlob[o]['Nom']].disabled = false;
			}
		}

		for (o = 0; o < (opensGlob.length); o++) {

			if (this[type + opensGlob[o]['Nom']].disabled === false) {
				this[type + opensGlob[o]['Nom']].checked = true;
			}
		}
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Vérifie que la touche pressée par l'utilisateur correspond à un chiffre.
 * @param evt - Evenement, touche pressée.
 * @returns {boolean}
 */
function isNumber(evt) {
	try {
		evt = (evt) ? evt : window.event;
		var charCode = (evt.which) ? evt.which : evt.keyCode;
		if (charCode > 31 && (charCode < 48 || charCode > 57)) {
			return false;
		}
		return true;
	}
	catch(error){
		message('error', error.message);
	}
}


var fired = false;
/**
 * Vérifie que la valeur entrée dans le champ Date De Naissance correspond bien à une date possible.
 * @param evt - Evenement, touche pressée.
 * @returns {boolean}
 */
function isDate(evt) {
	try {
		evt = (evt) ? evt : window.event;
		var charCode = (evt.which) ? evt.which : evt.keyCode;
		var c = document.getElementById('birthdate');

		if (charCode < 31 || (charCode > 48 && charCode < 57) || (charCode > 95 && charCode < 106)) {
			if ((c.value.length === 2 || c.value.length === 3) && (charCode < 48 || charCode > 49) && (charCode < 96 || charCode > 97) && charCode !== 8) {
				return false;
			}

			if (c.value.length === 4) {
				if (c.value.charAt(3) === '0' && (charCode < 49 || charCode > 57)) {
					return false;
				}
				if (c.value.charAt(3) === '1' && (charCode < 48 || charCode > 50)) {
					return false;
				}
			}
			if (fired === false) {
				if (charCode !== 8) {
					fired = true;
				}
				return true;
			}
		} else {
			return false;
		}
		return false;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Complète le champ Date De Naissance au fur et à mesure de la saisie de l'utilisateur.
 * @param evt - Evenement, touche pressée.
 */
function date(evt){
	try {
		evt = (evt) ? evt : window.event;
		var charCode = (evt.which) ? evt.which : evt.keyCode;
		var field = document.getElementById('birthdate');

		if (field.value.length === 2 || field.value.length === 5) {
			if (charCode !== 8) {
				field.value = field.value + '/';
			}
		} else if (field.value.length === 3) {
			if (charCode === 8) {
				field.value = field.value.slice(0, 2);
			} else {
				var char = field.value.charAt(2);
				if (char !== '/') {
					field.value = field.value.slice(0, 2) + "/" + char;
				}
			}
		} else if (field.value.length === 6) {
			if (charCode === 8) {
				field.value = field.value.slice(0, 5);
			} else {
				var char = field.value.charAt(5);
				field.value = field.value.slice(0, 5) + "/" + char;
			}
		}

		fired = false;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Vérifie que la touche pressée par l'utilisateur correspond à une lettre.
 * @param evt - Evenement, touche pressée.
 * @returns {boolean}
 */
function isLetter(evt) {
	try {
		evt = (evt) ? evt : window.event;
		var charCode = (evt.which) ? evt.which : evt.keyCode;
		if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
			return false;
		}
		return true;
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Récupère le valeur du champs NrFFE et enclenche le processus d'auto-complétion.
 */
function startCompletion(){
	try {
		var c = document.getElementById('licence');
		if (c.value.length === 6) {
			var res = c.value.toUpperCase();
			autoComp(res);
		}
		else {
			resetFields(false);
		}
	}
	catch(error){
		message('error', error.message);
	}
}

/**
 * Met à jour les frais d'inscription du joueur dynamiquement.
 */
function setFrais(){
	try {
		var frais = document.getElementById('frais');
		var NeLe = document.getElementById('birthdate').value.split("/");
		var date = new Date(NeLe[2], NeLe[1], NeLe[0]);
		var repas = document.getElementById('meal').value;
		var age = new Date().getFullYear() - date.getFullYear();

		var res = 0;
		if (isNaN(age) === false) {
			if (age < limiteAge)
				res = prixJeune;
			else
				res = prixVieux;
		}

		res += parseFloat(repas.split('|')[1]);

		frais.innerHTML = res.toString();
	}
	catch(error){
		message('error', error.message);
	}
}


/**
 * Affiche des message d'erreurs liés au remplissage manuel du formulaire.
 * @param type (string) - Type de message (erreur ou succès), seules les erreurs sont affichées.
 * @param text (string) - Message à afficher.
 */
function message(type, text){
	var message = document.getElementById("jsmessage");

	if(type === 'success') {
		message.className = "";
		message.innerHTML = "";
	}
	else if(type === 'error') {
		message.className = "message error";
		message.innerHTML = text;
	}
}

