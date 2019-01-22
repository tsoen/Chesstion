<?php
require_once 'controllers/controller_base.php';
require_once 'models/model_base.php';
require_once 'files/utils.php';
require_once 'files/config.php';
require_once 'models/joueur.php';
require_once 'models/mail.php';

/**
 * Class Controller_Home
 * Gère les requêtes et redirige vers la page demandée après traitement.
 */
class Controller_Home extends Controller_Base
{
	public function __construct() {}

	/**
	 * Requête vers '/', '/index.php' ou '/index.php/home', affiche home.php.
	 */
	public function index()
	{
		// Définition de svariables de session.
		if(!isset($_SESSION['output']))
			$_SESSION['output'] = -1;
		else
			$_SESSION['output'] = 0;
		$_SESSION['ref'] = -1;

		// Affichage de la liste des tournois existants.
		$listTournoi = scandir('json/Tournois');
		$this->render_view('home', ['list' => $listTournoi]);
	}

	/**
	 * Page principale d'un tournoi contenant les informations sur le tournoi et le formulaire d'inscription.
	 * @param $ref (int) - Référence du tournoi.
	 */
	public function inscription($ref){
		// Vérifie qu'il existe un tournoi avec cette référence.
		if(!file_exists("json/Tournois/" . $ref . ".json")){
			$this->message('error', "Le tournoi demandé n'existe pas. Veuillez sélectionné un tournoi ci-dessous");
			$this->redirect('home');
		}

		/**
		 * Requête classique via l'url de la page.
		 */
		if ($_SERVER['REQUEST_METHOD'] == 'GET') {

			try {
				// Définition des variables de session.
				if(!isset($_SESSION['output']) || $_SESSION['output'] == 1)
					$_SESSION['output'] = 0;

				if(isset($_GET['modeFFE']) && $_GET['modeFFE'] == 1){
					if(!isset($_SESSION['modeFFE']))
						$_SESSION['modeFFE'] = true;
					else
						$_SESSION['modeFFE'] = !$_SESSION['modeFFE'];

					$this->redirect('home/inscription/'.$ref);
				}

				$_SESSION['ref'] = $ref;

				// Passage d'une fonction à exécuter via l'url.
				if (isset($_GET['fonction'])) {
					switch ($_GET['fonction']) {
						// Fonction déclenchée par l'auto-complétion.
						case 'getInfo':
							if (isset($_GET['NrFFE'])) {
								$info = Joueur::getPlayerInfo($_GET['NrFFE']);
								echo json_encode($info);
								exit;
							}
					}
				}

				// Lecture des caractéristiques du tournoi dans le fichier Json correspondant.
				$jsonTournoi = file_get_contents("json/Tournois/" . $ref . ".json");
				$tournoi = json_decode($jsonTournoi, true);

				if($tournoi['Etat'] > 2) {
                    $this->message('error', "Les inscriptions sont terminées pour ce tournoi.");
                }

				// Affichage de la page du tournoi.
				$this->render_view('inscription', ['tournoi' => $tournoi]);
			}
			catch(Exception $ex)
			{
				// Redirection vers la page d'accueil et affichage du message d'erreur.
				$this->message('error', $ex -> getMessage());
				$this->redirect('home');
			}
		}

		/**
		 * Requête déclenchée via le formulaire d'inscription.
		 */
		if ($_SERVER['REQUEST_METHOD'] == 'POST'){
			try
			{
				// Convertit les caractères spéciaux en entités HTML de toutes les variables POST.
				check_post_values($_POST);

				// Sauvegarde les données entrées dans le formulaire.
				saveFormPost($_POST);

				// Contrôle du format de l'email.
				if (!(isset($_POST['email']) && filter_var($_POST['email'], FILTER_VALIDATE_EMAIL)))
					throw new Exception('Adresse email invalide.');

				// Erreur pour email SFR.
				if(preg_match("#^[a-z0-9._-]+@(sfr).[a-z]{2,4}$#", $_POST['email']))
					throw new Exception("SFR ne permet pas de recevoir d'email depuis nos services. Veuillez entrer une 
                nouvelle adresse email.");

				// Requête vers Google du reCaptcha.
				/*$url = 'https://www.google.com/recaptcha/api/siteverify';
				$data = ['secret'   => '6LeeMg4UAAAAAEczgKH5Hkn-3x7dNttNRch_EgKt',
					'response' => $_POST['g-recaptcha-response'],
					'remoteip' => $_SERVER['REMOTE_ADDR']];
				$options = [
					'http' => [
						'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
						'method'  => 'POST',
						'content' => http_build_query($data)
					]
				];
				$context  = stream_context_create($options);
				$result = file_get_contents($url, false, $context);
				// Vérification du reCaptcha.
				if (json_decode($result)->success == false)
					throw new Exception("Le Captcha n'a pas été validé.");*/

				// Lecture des caractéristiques du tournoi dans le fichier Json correspondant.
				$jsonTournoi = file_get_contents("json/Tournois/" . $ref . ".json");
				$tournoi = json_decode($jsonTournoi, true);

				// Erreur si inscriptions fermées.
				if($tournoi['Etat'] > 2)
					throw new Exception("Les inscriptions sont terminées pour ce tournoi.");

				// Lecture des inscrits dans le fichier Json correspondant.
				$file = 'json/Inscrits/' . $ref . '.json';
				if(file_exists('json/Inscrits/' . $ref . '.json'))
					$jsonInscrits = file_get_contents($file);
				else{
					// Création du fichier inscrits/ref si il n'existe pas.
					file_put_contents($file, "");
					$jsonInscrits = file_get_contents($file);
				}
				$inscrits = json_decode($jsonInscrits, true);

				// Erreur si nombre maximum de participants atteint pour ce tournoi.
				if (count($inscrits) >= $tournoi['NbParticipants'])
					throw new Exception("Inscription impossible : le nombre maximum d'inscrits a déjà été atteint.");

				// Détection de l'Elomax de l'open sélectionné, -1 si erreur.
				$elomax = -1;
				foreach ($tournoi['Opens'] as $open) {
					if ($open['Ref'] == $_POST['open'])
						$elomax = $open['EloMax'];
				}

				// Traitement pour joueur avec NrFFE.
				if(isset($_POST['licence']))
				{
					if (!(strlen($_POST['licence']) == 6))
						throw new Exception('Numéro FFE invalide.');

					$_POST['licence'] = strtoupper($_POST['licence']);

					// Récupère toutes les informations du joueur.
					$joueur = Joueur::getAllPlayerInfo($_POST['licence']);

					// Erreur si l'Elo du joueur est supérieur à l'Elomax de l'open sélectionné.
					if (!((int)$joueur[0]['Elo'] < $elomax) && $elomax != -1)
						throw new Exception("Elo incompatible avec Open sélectionné");

					// Ajout des informations complémentaires.
					$joueur[0]['Email'] = $_POST['email'];
					if (isset($_POST['open']))
						$joueur[0]['OpenRef'] = $_POST['open'];
					if (isset($_POST['phone']))
						$joueur[0]['Phone'] = $_POST['phone'];
					if (isset($_POST['meal'])) {
						$res = explode('|', $_POST['meal']);
						$joueur[0]['RepasRef'] = $res[0];
					}
					if($_POST['alertDevices'] == 'on')
						$joueur[0]['Subscribe'] = true;
					else
						$joueur[0]['Subscribe'] = false;

					// Erreur si le joueur est déjà inscrit au tournoi.
					if($inscrits != "") {
						foreach ($inscrits as $inscrit) {
							if ($inscrit['NrFFE'] == $joueur[0]['NrFFE'])
								throw new Exception("Vous êtes déjà inscrit à ce tournoi. Contactez l'organisateur si 
							vous rencontrez un problème.");
						}
					}

					// Si le fichier est vide, écriture du joueur tel quel.
					if (strlen($jsonInscrits) == 0)
						file_put_contents($file, json_encode($joueur));
					// Sinon, concaténation du tableau des inscrits existant avec le nouveau joueur.
					else
					{
						$jsonInscrits = substr($jsonInscrits, 0, strlen($jsonInscrits) - 1) . ", \n";

						$nouvelInscrit = json_encode($joueur);
						$nouvelInscrit = substr($nouvelInscrit, 1, strlen($nouvelInscrit));

						$new = $jsonInscrits . $nouvelInscrit;

						file_put_contents($file, $new);
					}

					// Mail de confirmation au joueur.
					Mail::mailToJoueur($joueur[0], $tournoi);

					// Mail de confirmation à l'arbitre s'il l'a demandé.
					if($tournoi['ConfirmMail'] == true)
						Mail::mailToArbitre($joueur[0], $tournoi);

					// Message de confirmation d'inscription.
					$this->message('success', 'Vous êtes correctement inscrit au tournoi: ' . $tournoi['Nom'] . '.
						 Un mail de confirmation vous a été envoyé à ' . $_POST['email'] . ". Pensez à vérifier vos 
						 spams si celui-ci n'apparaît pas.");

				}
				// Traitement pour joueur sans NrFFE.
				else{
					// Erreur si l'Elo du joueur est supérieur à l'Elomax de l'open sélectionné.
					if (!((int)$_POST['elo'] < $elomax) && $elomax != -1)
						throw new Exception("Elo incompatible avec Open sélectionné");

					// Récupération des informations entrées dans le formulaire.
					$infos = array();
					$infos[0]['Nom'] = $_POST['name'];
					$infos[0]['Prenom'] = $_POST['firstName'];
					$infos[0]['Elo'] = $_POST['elo'];
					$infos[0]['NeLe'] = $_POST['birthdate'];
					$infos[0]['Sexe'] = $_POST['sexe'];
					$infos[0]['Federation'] = $_POST['country'];
					$infos[0]['OpenRef'] = $_POST['open'];

					$res = explode('|', $_POST['meal']);
					$infos[0]['RepasRef'] = $res[0];
					$infos[0]['Email'] = $_POST['email'];
					$infos[0]['Phone'] = $_POST['phone'];
					if($_POST['alertDevices'] == 'on')
						$infos[0]['Subscribe'] = true;
					else
						$infos[0]['Subscribe'] = false;

					// Mail de demande d'inscription à l'arbitre.
					Mail::mailToArbitreNoFFE($infos[0], $tournoi);

					// Message de confirmation d'inscription.
					$this->message('success', "Un mail à bien été envoyé à l'organisateur du tournoi : " . $tournoi['Nom']
						. '. Il vous confirmera votre inscription dès que possible.');
				}

				// Si inscription réussie, oubli des valeurs du formulaire.
				unsetFormPost($_POST);

				// Redirige vers la page principale du tournoi et affiche un message de confirmation.
				$this->redirect('home/inscription/'.$ref);
			}
			catch(Exception $ex){
				// Redirige vers la page principale du tournoi et affiche un message d'erreur.
				$this->message('error', $ex->getMessage());
				$this->redirect('home/inscription/'.$ref);
			}
		}
	}

	/**
	 * Page qui affiche la liste des joueurs actuellement inscrits au tournoi.
	 * @param $ref (int) - Référence du tournoi.
	 */
	public function inscrits($ref){
		// Vérifie qu'il existe un tournoi avec cette référence.
		if(!file_exists("json/Tournois/" . $ref . ".json")){
			$this->message('error', "Le tournoi demandé n'existe pas. Veuillez sélectionné un tournoi ci-dessous");
			$this->redirect('home');
		}

		/**
		 * Requête classique via l'url de la page.
		 */
		if ($_SERVER['REQUEST_METHOD'] == 'GET') {
			try {
				// Définition des variables de session.
				if(!isset($_SESSION['output'])) {
					$_SESSION['output'] = 0;
					$this->redirect('home/inscription/' . $ref);
				}
				$_SESSION['ref'] = $ref;

				// Lecture des caractéristiques du tournoi dans le fichier Json correspondant.
				$jsonTournoi = file_get_contents("json/Tournois/" . $ref . ".json");
				$tournoi = json_decode($jsonTournoi, true);

				// Lecture des inscrits dans le fichier Json correspondant.
				if(file_exists("json/Inscrits/" . $ref . ".json")) {
					$jsonInscrits = file_get_contents("json/Inscrits/" . $ref . ".json");
					$inscrits = json_decode($jsonInscrits, true);
				}
				else
					$inscrits = null;

				// Affiche la page inscrits.
				$this->render_view('inscrits', ['tournoi' => $tournoi, 'inscrits' => $inscrits]);

			} catch (Exception $ex) {
				// Redirige vers la page d'accueil du site et affiche un message d'erreur.
				$this->message('error', $ex->getMessage());
				$this->redirect('home');
			}
		}
	}

	/**
	 * Page qui affiche les fichiers de résultats générés par Papi.
	 * @param $ref (int) - Référence du tournoi.
	 */
	public function resultats($ref){
		// Vérifie qu'il existe un tournoi avec cette référence.
		if(!file_exists("json/Tournois/" . $ref . ".json")){
			$this->message('error', "Le tournoi demandé n'existe pas. Veuillez sélectionner un tournoi ci-dessous");
			$this->redirect('home');
		}

		/**
		 * Requête classique via l'url de la page.
		 */
		if ($_SERVER['REQUEST_METHOD'] == 'GET') {
			try {
				// Définition des variables de session.
				if(!isset($_SESSION['output'])) {
					$_SESSION['output'] = 0;
					$this->redirect('home/inscription/' . $ref);
				}
				$_SESSION['ref'] = $ref;

				// Lecture des caractéristiques du tournoi dans le fichier Json correspondant.
				$jsonTournoi = file_get_contents("json/Tournois/" . $ref . ".json");
				$tournoi = json_decode($jsonTournoi, true);

				// Erreur si le tournoi n'a pas commencé.
				if($tournoi['Etat'] < 3){
					$this->message('error', "Les resultats ne sont pas encore disponibles pour ce tournoi.");
					$this->redirect('home/inscription/' . $ref);
				}

				// Lecture de la liste des fichiers générés par Papi.
				if(file_exists('html/' . $ref))
					$list = scandir('html/' . $ref);
				else
					$list = null;

				// Affiche la page de résultats.
				$this->render_view('resultats', ['list' => $list]);

			} catch (Exception $ex) {
				// Redirige vers la page d'accueil du site et affiche un message d'erreur.
				$this->message('error', $ex->getMessage());
				$this->redirect('home');
			}
		}
	}
}