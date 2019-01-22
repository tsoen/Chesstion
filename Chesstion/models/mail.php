<?php

class Mail
{
	/**
	 * Envoie un email au joueur pour confirmer son inscription et rappelle les détails du tournoi.
	 * @param $joueur (array) - Informations relatives au joueur.
	 * @param $tournoi (array) - Informations relatives au tournoi.
	 */
	public static function mailToJoueur($joueur, $tournoi){
		try {
			$mail = $joueur['Email'];

			// On filtre les serveurs qui rencontrent des bogues.
			if (!preg_match("#^[a-z0-9._-]+@(hotmail|live|msn).[a-z]{2,4}$#", $mail))
			{
				$passage_ligne = "\r\n";
			}
			else
			{
				$passage_ligne = "\n";
			}

			//=====Déclaration du message au format texte.
			//Détection du nom de l'Open
			$Open = null;
			foreach ($tournoi['Opens'] as $o) {
				if ($o['Ref'] == $joueur['OpenRef'])
					$Open = $o;
			}

			//Détection du nom du Repas
			$Repas = null;
			foreach ($tournoi['Repas'] as $r) {
				if ($r['Ref'] == $joueur['RepasRef'])
					$Repas = $r;
			}

			//Calcul frais d'inscription
			$Age = date('Y') - date('y', $joueur['NeLe']);
			$Frais = null;
			if($Age < $tournoi['LimiteAge'])
				$Frais = $tournoi['PrixJeune'];
			else
				$Frais = $tournoi['PrixVieux'];
			$Frais += $Repas['Prix'];


			$message_txt = "Bonjour,".$passage_ligne
				. "votre inscription a bien été prise en compte pour le tournoi : ".$tournoi['Nom']."." . $passage_ligne
				. "Déroulement du tournoi : du ".$tournoi['DateDebut']." au ".$tournoi['DateFin'] .".". $passage_ligne
				. "Adresse : ".$tournoi['NomLieu'].", ".$tournoi['AdresseNum']." ".$tournoi['AdresseRue'].", ".$tournoi['Ville']."."
				. $passage_ligne . $passage_ligne
				. "Vous êtes actuellement inscrit à l'Open ".$Open['Nom'].".". $passage_ligne
				. "Vos frais d'inscription s'élèvent à ".$Frais." €";

			if($Repas['Ref'] != -1)
				$message_txt.= " dont un repas (".$Repas['Nom'].") à ".$Repas['Prix']." €.";
			else
				$message_txt.= ".". $passage_ligne. "Vous n'avez pas réservé de repas.". $passage_ligne;

			//==========

			//=====Création de la boundary
			$boundary = "-----=".md5(rand());
			//==========

			//=====Définition du sujet.
			$sujet = "Récapitulatif de votre inscription.";
			//=========

			//=====Création du header de l'e-mail.
			$header = "From: \"Cercle d'Echecs de la Thur\"<Chesstion.noreply@gmail.com>".$passage_ligne;
			$header.= "Reply-to: \"Cercle d'Echecs de la Thur\" <Chesstion.noreply@gmail.com>".$passage_ligne;
			$header.= "MIME-Version: 1.0".$passage_ligne;
			$header.= "Content-Type: multipart/alternative;".$passage_ligne." boundary=\"$boundary\"".$passage_ligne;
			//==========

			//=====Création du message.
			$message = $passage_ligne."--".$boundary.$passage_ligne;
			//=====Ajout du message au format texte.
			$message.= "Content-Type: text/plain; charset=\"UTF-8\"".$passage_ligne;
			$message.= "Content-Transfer-Encoding: 8bit".$passage_ligne;
			$message.= $passage_ligne.$message_txt.$passage_ligne;
			//==========
			$message.= $passage_ligne."--".$boundary.$passage_ligne;
			//==========

			//=====Envoi de l'e-mail.
			mail($mail,$sujet,$message,$header);
			//==========

		} catch (Exception $e) {
			echo $e->getMessage();
			exit;
		}
	}

	/**
	 * Envoie un email à l'arbitre pour lui indiquer qu'un joueur sans NrFFE souhaite s'inscrire.
	 * @param $joueur (array) - Informations relatives au joueur.
	 * @param $tournoi (array) - Informations relatives au tournoi.
	 */
	public static function mailToArbitreNoFFE($joueur, $tournoi)
	{
		try
		{
			$mail = $tournoi['Arbitre'];

			// On filtre les serveurs qui rencontrent des bogues.
			if (!preg_match("#^[a-z0-9._-]+@(hotmail|live|msn).[a-z]{2,4}$#", $mail))
			{
				$passage_ligne = "\r\n";
			} else {
				$passage_ligne = "\n";
			}

			//=====Déclaration du message au format texte.
			//Détection du nom de l'Open
			$Open = null;
			foreach ($tournoi['Opens'] as $o) {
				if ($o['Ref'] == $joueur['OpenRef'])
					$Open = $o;
			}

			//Détection du nom du Repas
			$Repas = null;
			foreach ($tournoi['Repas'] as $r) {
				if ($r['Ref'] == $joueur['RepasRef'])
					$Repas = $r;
			}

			$message_txt = "Un joueur souhaite s'inscrire à un tournoi :" . $passage_ligne
				. "- Tournoi : ".$tournoi['Nom'] . $passage_ligne
				. "- Open : ".$Open['Nom'] . $passage_ligne
				. $passage_ligne
				. "Informations relatives au joueur : " . $passage_ligne
				. "- Nom : " .$joueur['Nom'] . $passage_ligne
				. "- Prenom : " .$joueur['Prenom'] . $passage_ligne
				. "- Sexe : " .$joueur['Sexe'] . $passage_ligne
				. "- NeLe : " .$joueur['NeLe'] . $passage_ligne
				. "- Elo : " .$joueur['Elo'] . $passage_ligne
				. "- Federation : " .$joueur['Federation'] . $passage_ligne
				. "- Email : " .$joueur['Email'] . $passage_ligne
				. "- Telephone : " .$joueur['Phone'] . $passage_ligne
				. "- Repas : " .$Repas['Nom'] . $passage_ligne;

			if ($joueur['Subscribe'] == 0)
				$message_txt .= "- Recevoir des informations par Email : Non" . $passage_ligne;
			else
				$message_txt .= "- Recevoir des informations par Email : Oui" . $passage_ligne;


			//=====Création de la boundary
			$boundary = "-----=" . md5(rand());
			//==========

			//=====Définition du sujet.
			$sujet = "Vous avez reçu une demande d'inscription.";
			//=========

			//=====Création du header de l'e-mail.
			$header = "From: \"Cercle d'Echecs de la Thur\"<Chesstion.noreply@gmail.com>" . $passage_ligne;
			$header .= "Reply-to: \"Cercle d'Echecs de la Thur\" <Chesstion.noreply@gmail.com>" . $passage_ligne;
			$header .= "MIME-Version: 1.0" . $passage_ligne;
			$header .= "Content-Type: multipart/alternative;" . $passage_ligne . " boundary=\"$boundary\"" . $passage_ligne;
			//==========

			//=====Création du message.
			$message = $passage_ligne . "--" . $boundary . $passage_ligne;
			//=====Ajout du message au format texte.
			$message .= "Content-Type: text/plain; charset=\"UTF-8\"" . $passage_ligne;
			$message .= "Content-Transfer-Encoding: 8bit" . $passage_ligne;
			$message .= $passage_ligne . $message_txt . $passage_ligne;
			//==========
			$message .= $passage_ligne . "--" . $boundary . $passage_ligne;
			//==========

			//=====Envoi de l'e-mail.
			mail($mail, $sujet, $message, $header);
			//==========

		} catch (Exception $e) {
			echo $e->getMessage();
			exit;
		}
	}

	/**
	 * Envoie un email à l'arbitre qui récapitule l'inscription d'un joueur possédant un NrFFE.
	 * @param $joueur (array) - Informations relatives au joueur.
	 * @param $tournoi (array) - Informations relatives au tournoi.
	 */
	public static function mailToArbitre($joueur, $tournoi)
	{
		try
		{
			$mail = $tournoi['Arbitre'];

			// On filtre les serveurs qui rencontrent des bogues.
			if (!preg_match("#^[a-z0-9._-]+@(hotmail|live|msn).[a-z]{2,4}$#", $mail))
			{
				$passage_ligne = "\r\n";
			} else {
				$passage_ligne = "\n";
			}

			//=====Déclaration du message au format texte.
			//Détection du nom de l'Open
			$Open = null;
			foreach ($tournoi['Opens'] as $o) {
				if ($o['Ref'] == $joueur['OpenRef'])
					$Open = $o;
			}

			//Détection du nom du Repas
			$Repas = null;
			foreach ($tournoi['Repas'] as $r) {
				if ($r['Ref'] == $joueur['RepasRef'])
					$Repas = $r;
			}

			$message_txt = "Un joueur s'est inscrit à un tournoi :" . $passage_ligne
				. "- Tournoi : ".$tournoi['Nom'] . $passage_ligne
				. "- Open : ".$Open['Nom'] . $passage_ligne
				. $passage_ligne
				. "Informations relatives au joueur : " . $passage_ligne
				. "- NrFFE : " .$joueur['NrFFE'] . $passage_ligne
				. "- Nom : " .$joueur['Nom'] . $passage_ligne
				. "- Prenom : " .$joueur['Prenom'] . $passage_ligne
				. "- Sexe : " .$joueur['Sexe'] . $passage_ligne
				. "- NeLe : " .$joueur['NeLe'] . $passage_ligne
				. "- Elo : " .$joueur['Elo'] . $passage_ligne
				. "- Federation : " .$joueur['Federation'] . $passage_ligne
				. "- Email : " .$joueur['Email'] . $passage_ligne
				. "- Telephone : " .$joueur['Phone'] . $passage_ligne
				. "- Repas : " .$Repas['Nom'] . $passage_ligne;

			if ($joueur['Subscribe'] == 0)
				$message_txt .= "- Recevoir des informations par Email : Non" . $passage_ligne;
			else
				$message_txt .= "- Recevoir des informations par Email : Oui" . $passage_ligne;


			//=====Création de la boundary
			$boundary = "-----=" . md5(rand());
			//==========

			//=====Définition du sujet.
			$sujet = "Recapitulation d'inscription.";
			//=========

			//=====Création du header de l'e-mail.
			$header = "From: \"Cercle d'Echecs de la Thur\"<Chesstion.noreply@gmail.com>" . $passage_ligne;
			$header .= "Reply-to: \"Cercle d'Echecs de la Thur\" <Chesstion.noreply@gmail.com>" . $passage_ligne;
			$header .= "MIME-Version: 1.0" . $passage_ligne;
			$header .= "Content-Type: multipart/alternative;" . $passage_ligne . " boundary=\"$boundary\"" . $passage_ligne;
			//==========

			//=====Création du message.
			$message = $passage_ligne . "--" . $boundary . $passage_ligne;
			//=====Ajout du message au format texte.
			$message .= "Content-Type: text/plain; charset=\"UTF-8\"" . $passage_ligne;
			$message .= "Content-Transfer-Encoding: 8bit" . $passage_ligne;
			$message .= $passage_ligne . $message_txt . $passage_ligne;
			//==========
			$message .= $passage_ligne . "--" . $boundary . $passage_ligne;
			//==========

			//=====Envoi de l'e-mail.
			mail($mail, $sujet, $message, $header);
			//==========

		} catch (Exception $e) {
			echo $e->getMessage();
			exit;
		}
	}
}