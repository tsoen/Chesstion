<?php

require_once 'files/config.php';
require_once 'models/model_base.php';
require_once 'controllers/controller_base.php';

define('BASEURL', dirname($_SERVER['SCRIPT_NAME']));

// init DB connection
try
{
	$uname = explode(" ", php_uname());
	$os = $uname[0];
	switch ($os){
		case 'Windows':
			$mdb_file = $_SERVER['DOCUMENT_ROOT'] . './Chesstion/DATA.MDB';
			$driver = '{Microsoft Access Driver (*.mdb, *.accdb)}';
			break;
		case 'Linux':
			$mdb_file = $_SERVER['DOCUMENT_ROOT'] . '/DATA.MDB';
			$driver = 'MDBTools';
            $driver = '{Microsoft Access Driver (*.mdb)}';
			break;
		default:
			exit("Don't know about this OS");
	}
	$connect_string = "Driver={$driver};DBQ={$mdb_file};";
	$dataSourceName = "odbc:" . $connect_string;
	$db = new PDO($dataSourceName);
	$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    // set static $db instance
    Model_Base::set_db($db);
}
catch (Exception $e) {
    try{
        $db = new PDO(PGSQL_DSN);
        $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        Model_Base::set_db($db);
    } catch (Exception $e) {
        $c = new Controller_Base();
        http_response_code(500);
        $c->render_view('errors/500');
        header('Location: views/errors/500.php');
        exit;
    }
}

session_set_cookie_params(10000, '/', '', false, true);
session_start();

date_default_timezone_set('Europe/Paris');
setlocale(LC_TIME, 'fr_FR.utf8','fra');

if(isset($_SERVER['PATH_INFO'])) {
	$args = explode('/', $_SERVER['PATH_INFO']);

	// if route = 'index.php' or 'index.php/' : load home
	if (count($args) == 1 || (count($args) == 2 && empty($args[1]))) {
		require_once 'controllers/home.php';
		$c = new Controller_Home();
		$c->index();
		exit;
	}

	else if (count($args) >= 2) {
		$controller = $args[1];

		// if route = 'index.php/ctrl/' : call index method of ctrl
		if (count($args) == 2 || (count($args) == 3 && empty($args[2]))) {
			$method = 'index';
		} else {
			$method = $args[2];
		}

		$params = array();
		for ($i = 3; $i < count($args); $i++) {
			$params[] = $args[$i];
		}

		$controller_file = dirname(__FILE__).'/controllers/'.$controller.'.php';
		if (is_file($controller_file)) {
			require_once $controller_file;
			// underscored to upper-camelcase
			// e.g. "this_controller_name" -> "ThisControllerName"
			$controller = preg_replace_callback('/(?:^|_)(.?)/', function($m) { return strtoupper($m[1]); }, $controller);
			$controller_name = 'Controller_'.$controller;
			if (class_exists($controller_name)) {
				$c = new $controller_name;
				if (method_exists($c, $method)) {
					call_user_func_array(array($c, $method), $params);
					exit;
				}
			}
		}
	}
} else {
	// if PATH_INFO is not defined : load home
	require_once 'controllers/home.php';
	$c = new Controller_Home();
	$c->index();
	exit;
}

// if we get here : return 404
$c = new Controller_Base();
http_response_code(404);
$c->message('error', "Erreur 404: la page demandée n'existe pas.");
$c->redirect('home');