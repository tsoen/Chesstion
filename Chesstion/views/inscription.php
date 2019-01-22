
<script type="text/javascript" xmlns="http://www.w3.org/1999/html">
	initialise(<?=json_encode($tournoi['Opens'])?>, <?=$_SESSION['ref']?>,
		<?=$tournoi['PrixVieux']?>,<?=$tournoi['PrixJeune']?>,<?=$tournoi['LimiteAge']?>);
</script>

<div class="col-md-3 far-left">
	<fieldset><legend><?=$tournoi['Nom']?></legend></fieldset>
	<p class="text-justify"><?=$tournoi['Nom']?> aura lieu du <strong>
		<?=DateTime::createFromFormat('d/m/Y', $tournoi['DateDebut'])->format('d');?> au
		<?php $myDateTime = DateTime::createFromFormat('d/m/Y', $tournoi['DateFin']);
		echo strftime(" %d %B %Y", $myDateTime->getTimestamp()); ?></strong> au
		<?=$tournoi['NomLieu']?> situé au <?=$tournoi['AdresseNum']." ". $tournoi['AdresseRue'].', '. $tournoi['Ville'] ?>.
	</p>
	<p class="text-justify">Il y aura <strong><?=$tournoi['NbOpens']?></strong> opens de <strong><?=$tournoi['NbRondes']?> rondes
		de <?= DateTime::createFromFormat('i:s', $tournoi['DureeRonde'])->format('i')?> min +
		<?= DateTime::createFromFormat('i:s', $tournoi['DureeRonde'])->format('s')?>s</strong>
		(limité aux <strong><?=$tournoi['NbParticipants']?></strong> premiers inscrits)
	</p>
	<ul>
		<?php
		foreach($tournoi['Opens'] as $i){
			if($i['EloMax'] == '-1')
				echo '<li>Open'. ' ' .$i['Nom'] . ' ouvert à tous</li>';
			else
				echo '<li>Open' . ' ' . $i['Nom'] . ' < ' . $i['EloMax'] . '</li>';
		} ?>
	</ul>
	<p class="text-justify">Les droits d’inscriptions s’élèvent à <?=$tournoi['PrixVieux']?> euros pour les adultes et
		<?=$tournoi['PrixJeune']?> euros pour les jeunes de moins de <?=$tournoi['LimiteAge']?> ans. Une majoration de
		<?=$tournoi['Majoration']?> euros sera appliquée pour toute inscription faite le jour même.
		L’inscription est gratuite pour les titrés (MF, MI, GMI)
	</p>
	<p>De nombreux prix sont prévus par catégories.</p>
	<p><strong>ATTENTION</strong>: le nombre de participants est limité à <?=$tournoi['NbParticipants']?>.</p>
	<div>
		<p class="inline">Vos frais d'inscription à régler sur place s'élèvent actuellement à </p><strong>
		<p class="inline" id="frais">0</p><p class="inline"> euros.</p></strong>
	</div>
	<br/>
	<p>Si vous rencontrez un problème, veuillez contacter l'organisateur à cette adresse : <?=$tournoi['Arbitre']?></p>
</div>

<div class="col-md-1"></div>

<form class="form-horizontal col-md-8">
	<fieldset>

		<!-- Form Name -->
		<legend>Formulaire</legend>

		<!-- Text input-->
		<div class="form-group ">
			<label class="col-md-4 control-label" for="licence">Numéro de licence FFE</label>
			<div class="col-md-4">
				<input id="licence" name="licence" <?php if(isset($_SESSION['licence'])) echo('value="'.$_SESSION['licence'].'"')?>
					   placeholder="" class="form-control input-md" required="" type="text" maxlength="6"
					   style="text-transform:uppercase" oninput="startCompletion()">
                <p>(Ex: H03781, C71376, K52564, L68913 ou encore N53302)</p>
				<a href="../inscription/<?=$_SESSION['ref']?>?modeFFE=1" onclick="switchMode(true)">
					Ma licence n'a pas de numéro FFE</a>
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="name">Nom</label>
			<div class="col-md-4">
				<input id="name" name="name" <?php if(isset($_SESSION['name'])) echo('value="'.$_SESSION['name'].'"')?>
					   placeholder="MARTIN" class="form-control input-md" type="text" style="text-transform:uppercase" disabled
					   onkeypress="return (isLetter(event))">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="firstName">Prénom</label>
			<div class="col-md-4">
				<input id="firstName" name="firstName" <?php if(isset($_SESSION['firstName'])) echo('value="'.$_SESSION['firstName'].'"')?>
					   placeholder="Jean" class="form-control input-md" type="text" style="text-transform:capitalize" disabled
					   onkeypress="return (isLetter(event))">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="club">Club</label>
			<div class="col-md-4">
				<input id="club" name="club" <?php if(isset($_SESSION['club'])) echo('value="'.$_SESSION['club'].'"')?>
					   placeholder="" class="form-control input-md" type="text" disabled>
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="ligue">Ligue</label>
			<div class="col-md-4">
				<input id="ligue" name="ligue" <?php if(isset($_SESSION['ligue'])) echo('value="'.$_SESSION['ligue'].'"')?>
					   placeholder="XXX" class="form-control input-md" type="text" maxlength="3" style="text-transform:uppercase" disabled
					   onkeypress="return (isLetter(event))">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="elo">ELO</label>
			<div class="col-md-4">
				<input id="elo" name="elo" <?php if(isset($_SESSION['elo'])) echo('value="'.$_SESSION['elo'].'"')?>
					   placeholder="XXXX" class="form-control input-md" type="text" maxlength="4" disabled
					   onkeypress="return (isNumber(event))" oninput="disableOpens(this.value)">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="birthdate">Date de naissance</label>
			<div class="col-md-4">
				<input id="birthdate" name="birthdate" <?php if(isset($_SESSION['birthdate'])) echo('value="'.$_SESSION['birthdate'].'"')?>
					   placeholder="DD/MM/YYYY" class="form-control input-md" type="text" maxlength="10" disabled
					   onkeydown="return(isDate(event))" onkeyup="date(event)" onchange="setFrais()">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="sexe">Sexe (M/F)</label>
			<div class="col-md-4">
				<input id="sexe" name="sexe" <?php if(isset($_SESSION['sexe'])) echo('value="'.$_SESSION['sexe'].'"')?>
					   placeholder="X" class="form-control input-md" type="text" maxlength="1" style="text-transform:uppercase" disabled
					   onkeypress="return (isLetter(event))">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group">
			<label class="col-md-4 control-label" for="country">Nationalité</label>
			<div class="col-md-4">
				<input id="country" name="country" <?php if(isset($_SESSION['country'])) echo('value="'.$_SESSION['country'].'"')?>
					   placeholder="XXX" style="text-transform:uppercase" class="form-control input-md" type="text" disabled>
			</div>
		</div>


		<!-- Multiple Radios (inline) -->
		<div class="form-group">
			<label class="col-md-4 control-label" for="open">Open</label>
			<div class="col-md-4">
				<?php
				foreach($tournoi['Opens'] as $i){
					echo '<label class="radio-inline" for="open-'.$i['Nom'].'">
                    <input name="open" id="open-'.$i['Nom'].'" value="'.$i['Ref'].'" checked="checked" type="radio">'
						.$i['Nom'].'</label> ';
				}?>
			</div>
		</div>

		<!-- Listbox Select -->
		<div class="form-group">
			<label class="col-md-4 control-label" for="meal">Repas</label>
			<div class="col-md-4">
				<select name="meal" id="meal" onchange="setFrais()">
					<?php
					foreach($tournoi['Repas'] as $r){
						if($r['Ref'] == -1){
							echo '<option value="'.$r['Ref'].'|'.$r['Prix'].'" selected>'.$r['Nom'].' - '.$r['Prix'].' €</option>';
						}
						else{
							echo '<option value="'.$r['Ref'].'|'.$r['Prix'].'">'.$r['Nom'].' - '.$r['Prix'].' €</option>';
						}
					}?>
				</select>
			</div>
		</div>

		<!-- Multiple Checkboxes (inline) -->
		<div class="form-group">
			<label class="col-md-4 control-label" for="alertDevices">Recevoir des informations liées au tournoi par email</label>
			<div class="col-md-4">
				<label class="checkbox-inline" for="alertDevices-0">
					<input name="alertDevices" <?php if(isset($_SESSION['alertDevices'])) echo('value="'.$_SESSION['alertDevices'].'"')?>
						   id="alertDevices-0" type="checkbox" checked>
				</label>
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group" id="alertDevices-email">
			<label class="col-md-4 control-label" for="email">Adresse email</label>
			<div class="col-md-4">
				<input id="email" name="email" <?php if(isset($_SESSION['email'])) echo('value="'.$_SESSION['email'].'"')?>
					   placeholder="" class="form-control input-md" type="text" required maxlength="40">
			</div>
		</div>

		<!-- Text input-->
		<div class="form-group" id="alertDevices-phone">
			<label class="col-md-4 control-label" for="phone">Téléphone portable (optionnel)</label>
			<div class="col-md-4">
				<input id="phone" name="phone" <?php if(isset($_SESSION['phone'])) echo('value="'.$_SESSION['phone'].'"')?>
					   placeholder="" class="form-control input-md" type="text" maxlength="20">
			</div>
		</div>

		<!-- Google ReCaptcha -->
		<div class="form-group">
			<label class="col-md-4 control-label" for=""></label>
			<div class="g-recaptcha col-md-4" data-sitekey="<?=DATA_SITEKEY?>"></div>
		</div>

		<!-- Button (Double) -->
		<div class="form-group">
			<label class="col-md-4 control-label" for="submit"></label>
			<div class="col-md-8">
				<button id="submit" name="submit" formaction="<?=$_SESSION['ref']?>" formmethod="post"
						class="btn btn-success" type="submit" <?php if($tournoi['Etat']>2)echo 'disabled'; ?> >S'inscrire</button>
				<button id="empty" name="empty" class="btn btn-inverse" onclick="resetFields(true)">Vider les champs</button>
			</div>
		</div>
	</fieldset>
</form>

<script>
	<?php if(isset( $_SESSION['modeFFE']) && $_SESSION['modeFFE'] == false){ ?>
		switchMode(false);
	<?php } ?>
	startCompletion();
</script>

<?php

// Supprime la sauvegarde des données du formulaire
if(isset($_SESSION['SavePost'])) {
	unsetFormPost($_SESSION['SavePost']);
}
?>