<?php
require_once 'model_base.php';

class Joueur extends Model_Base
{
	/**
	 * Retourne les infos du Joueur trouvées dans la base nécessaires à l'auto-complétion.
	 * @param $NrFFE (string) - NrFFE du joueur.
	 * @return array - Informations du joueur.
	 */
	public static function getPlayerInfo($NrFFE)
	{
		try {
			// Erreur si format du NrFFE invalide.
			if (!preg_match('/[a-zA-Z][0-9]{5}/', $NrFFE)) {
				throw new Exception("NrFFE invalide.");
			}

			// Erreur si il n'existe pas de joueur avec ce NrFFE
			if (!self::playerExists($NrFFE)) {
				throw new Exception("Ce NrFFE n'existe pas.");
			}

			$tabLists = array();

			// Recherche du joueur avec ce NrFFE
			$query = "SELECT * FROM JOUEUR WHERE NrFFE = '$NrFFE'";
			$result = self::$_db->query($query);


			foreach ($result as $row) {
				$result->closeCursor();

                $row = array_change_key_case($row, CASE_UPPER);

                $NeLe = DateTime::createFromFormat("d/m/Y H:i:s", $row['NELE']);
                if(!$NeLe){
                    $NeLe = DateTime::createFromFormat("Y-m-d H:i:s", $row['NELE']);
                }

                $tabLists['Nom'] = $row['NOM'];
				$tabLists['Prenom'] = $row['PRENOM'];
				$tabLists['Elo'] = $row['ELO'];
				$tabLists['NeLe'] = $NeLe->format('d/m/Y');
				$tabLists['Sexe'] = $row['SEXE'];
				$tabLists['Federation'] = $row['FEDERATION'];
				$tabLists['ClubRef'] = self::getNomClub($row['CLUBREF']);;

				$tabLists['Ligue'] = self::getLigueClub($row['CLUBREF']);;
			}

			return $tabLists;

		}
		// Les erreurs seront affichés par par le javascript (voir completeFields()).
		catch (PDOException $e) {
			// Erreur de connexion à la base.
			echo 'Connection failed: ' . $e->getMessage();
			exit;
		} catch (Exception $e) {
			// Autres erreurs.
			echo($e->getMessage());
			exit;
		}
	}

	/**
	 * Retourne toutes les infos du Joueur trouvées dans la base.
	 * @param $NrFFE (string) - NrFFE du joueur.
	 * @return array - Informations du joueur.
	 * @throws Exception
	 */
	public static function getAllPlayerInfo($NrFFE){
		try {
			// Erreur si format du NrFFE invalide.
			if (!preg_match('/[a-zA-Z][0-9]{5}/', $NrFFE))
				throw new Exception("NrFFE invalide.");

			// Erreur si il n'existe pas de joueur avec ce NrFFE
			if(!self::playerExists($NrFFE))
				throw new Exception("Ce NrFFE n'existe pas.");

			$tabLists = array();

			// Recherche du joueur avec ce NrFFE
			$query = "SELECT * FROM JOUEUR WHERE NrFFE = '$NrFFE'";
			$result = self::$_db->query($query);

			foreach($result as $row) {
				$result->closeCursor();

                $row = array_change_key_case($row, CASE_UPPER);

				$NeLe = DateTime::createFromFormat("d/m/Y H:i:s", $row['NELE']);
                if(!$NeLe){
                    $NeLe = DateTime::createFromFormat("Y-m-d H:i:s", $row['NELE']);
                }

				array_push($tabLists, ['Ref' => $row['REF'], 'NrFFE' => $row['NRFFE'], 'Nom' => $row['NOM'],
					'Prenom' => $row['PRENOM'], 'Sexe' => $row['SEXE'], 'NeLe' => $NeLe->format('d/m/Y'),
					'Cat' => $row['CAT'], 'Federation' => $row['FEDERATION'], 'ClubRef' => $row['CLUBREF'],
					'Elo' => $row['ELO'], 'Rapide' => $row['RAPIDE'], 'Fide' => $row['FIDE'], 'FideCode' => $row['FIDECODE'],
					'FideTitre' => $row['FIDETITRE'], 'AffType' => $row['AFFTYPE'], 'Actif' => $row['ACTIF']]);
			}

			return $tabLists;
		}
		// Ces erreurs seront à nouveau catch dans le mode POST de controller/home/inscription
		catch (PDOException $e) {
			// Erreur de connexion à la base.
			throw new Exception('Connection failed: ' . $e->getMessage());
		} catch (Exception $e) {
			// Autres erreurs.
			throw new Exception($e->getMessage());
		}
	}

	/**
	 * Renvoie le nom du club dont la ref est passée en paramètre.
	 * @param $clubRef (string) - Reference du club.
	 * @return string - Nom du club.
	 */
	public static function getNomClub($clubRef){
		try{
			// Erreur si il n'existe pas de club avec cette référence
			if(!self::clubExists($clubRef))
				throw new Exception("Ce club n'est pas référencé. Veuillez contacter l'organisateur.");

			// Recherche du club avec cette référence
			$query = "SELECT * FROM CLUB WHERE Ref = $clubRef";
			$result = self::$_db->query($query);
			$res = "";

			foreach($result as $row) {
				$result->closeCursor();
                $row = array_change_key_case($row, CASE_UPPER);
				$res = $row['NOM'];
			}

			return $res;
		}
		// Les erreurs seront affichés par par le javascript (voir completeFields()).
		catch (PDOException $e) {
			// Erreur de connexion à la base.
			echo 'Connection failed: ' . $e->getMessage();
			exit;
		} catch (Exception $e) {
			// Autres erreurs.
			echo ($e->getMessage());
			exit;
		}
	}

	/**
	 * Renvoie la ligue du club dont la ref est passée en paramètre.
	 * @param $clubRef (string) - Reference du club.
	 * @return string - Ligue du club.
	 */
	public static function getLigueClub($clubRef){
		try{
			// Erreur si il n'existe pas de club avec cette référence
			if(!self::clubExists($clubRef))
				throw new Exception("Ce club n'est pas référencé. Veuillez contacter l'organisateur.");

			// Recherche du club avec cette référence
			$query = "SELECT * FROM CLUB WHERE Ref = $clubRef";
			$result = self::$_db->query($query);
			$res = "";

			foreach($result as $row) {
				$result->closeCursor();
                $row = array_change_key_case($row, CASE_UPPER);
				$res = $row['LIGUE'];
			}

			return $res;
		}
		// Les erreurs seront affichés par par le javascript (voir completeFields()).
		catch (PDOException $e) {
			// Erreur de connexion à la base.
			echo 'Connection failed: ' . $e->getMessage();
			exit;
		} catch (Exception $e) {
			// Autres erreurs.
			echo ($e->getMessage());
			exit;
		}
	}

	/**
	 * Vérifie qu'il existe un joueur portant ce NrFFE dans la base.
	 * @param $NrFFE (string) - NrFFE du joueur.
	 *
	 * @return bool - true si le joueur existe, false sinon.
	 */
	public static function playerExists($NrFFE){
		try{
			// Recherche du club avec cette référence
			$query = "SELECT * FROM JOUEUR WHERE NrFFE = '$NrFFE'";
			$result = self::$_db->query($query);

			$count = count($result->fetchAll());

			if($count == 1)
				return true;
			else
				return false;

		}
		// Les erreurs seront affichés par par le javascript (voir completeFields()).
		catch (PDOException $e) {
			// Erreur de connexion à la base.
			echo 'Connection failed: ' . $e->getMessage();
			exit;
		} catch (Exception $e) {
			// Autres erreurs.
			echo ($e->getMessage());
			exit;
		}
	}

	/**
	 * Vérifie qu'il existe un club portant cette référence dans la base.
	 * @param $clubRef (string) - Référence du club.
	 *
	 * @return bool - true si le club existe, false sinon.
	 */
	public static function clubExists($clubRef){
		try{
			// Recherche du club avec cette référence
			$query = "SELECT * FROM CLUB WHERE Ref = $clubRef";
			$result = self::$_db->query($query);

			$count = count($result->fetchAll());

			if($count == 1)
				return true;
			else
				return false;

		}
		// Les erreurs seront affichés par par le javascript (voir completeFields()).
		catch (PDOException $e) {
			// Erreur de connexion à la base.
			echo 'Connection failed: ' . $e->getMessage();
			exit;
		} catch (Exception $e) {
			// Autres erreurs.
			echo ($e->getMessage());
			exit;
		}
	}
}