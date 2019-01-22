<?php

class Controller_Base
{
    public function __construct() {}

	/**
	 * Redirige vers la page demandée.
	 * @param $target (string) - Page à afficher.
	 */
    public function redirect($target) {
        $t = BASEURL . '/index.php/' . $target;

        header('Location: ' . $t);
        exit;
    }

	/**
	 * Affiche une page et transmet les informations passées en paramètre.
	 * @param       $viewname (string) - Nom de la vue à afficher.
	 * @param array $data (array) - Tableau de paramètre à passer à la vue.
	 */
    public function render_view($viewname, Array $data = array()) {
        extract($data);
        $viewfile = 'views/' . $viewname . '.php';
        ob_start();
        if (is_readable($viewfile)) {
            require_once $viewfile;
        }
        $content = ob_get_clean();

        require_once 'views/main.php';
        exit;
    }

	/**
	 * Créer une variable de session utilisée pour afficher les messages de succès/d'erreur.
	 * @param $type (string) - "success" ou "error".
	 * @param $text (string) - Texte à afficher.
	 */
	public function message($type, $text) {
		$_SESSION['message'] = array(
			'type' => $type,
			'text' => $text
		);
	}
}


