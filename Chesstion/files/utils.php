<?php

/**
 * Convertit les caractères spéciaux en entités HTML de toutes les variables POST.
 * @param array $var - Tableau contenant les valeurs à convertir.
 *
 * @return bool
 */
function check_post_values(Array $var) {
	foreach ($var as $v) {
		if (!isset($_POST[$v])) {
			return false;
		} else {
			$_POST[$v] = htmlspecialchars($_POST[$v]);
		}
	}
	return true;
}


/**
 * Sauvegarde les valeurs entrées dans le formulaire pour les réafficher après une erreur lors de la confirmation.
 * @param array $var - Tableau contenant les valeurs entrée dans le formulaire.
 *
 * @return bool
 */
function saveFormPost(Array $var){
	foreach ($var as $key => $value) {
		if (!isset($_POST[$key])) {
			return false;
		} else {
			$_SESSION[$key] = $_POST[$key];
		}
	}
	$_SESSION['SavePost'] = $var;
	return true;
}

/**
 * Annule la sauvegarde des valeurs entrées dans le formulaire.
 * @param array $var - Tableau contenant les valeurs à oublier.
 */
function unsetFormPost(Array $var){
	foreach ($var as $key => $value) {
		if($key != 'meal')
			unset($_SESSION[$key]);
	}
}

/**
 * Utilisé par la fonction usort() dans inscrits.php, permet de trier les inscrits par Elo décroissant.
 * @param $a
 * @param $b
 *
 * @return mixed
 */
function cmp($a, $b) {
	return $b['Elo'] - $a['Elo'];
}

