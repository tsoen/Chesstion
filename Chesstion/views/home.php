
<div class="homeLists">
	<div class="keeptogether">
		<h3>Inscriptions ouvertes</h3>
		<ul>
			<?php
			foreach ($list as $tournoi){
				if($tournoi != '.' && $tournoi != '..'){
					$content = file_get_contents("json/Tournois/$tournoi");
					$json = json_decode($content, true);
					if($json['Etat'] == 2)
						echo '<li><a href="' . BASEURL . '/index.php/home/inscription/'.$json['Ref'].'">'.$json['Nom'].'</a></li></br>';
				}
			}
			?>
		</ul>
	</div>

	<div class="keeptogether">
		<h3>Tournois en cours</h3>
		<ul>
			<?php
			foreach ($list as $tournoi){
				if($tournoi != '.' && $tournoi != '..'){
					$content = file_get_contents("json/Tournois/$tournoi");
					$json = json_decode($content, true);
					if($json['Etat'] == 3)
						echo '<li><a href="' . BASEURL . '/index.php/home/inscription/'.$json['Ref'].'">'.$json['Nom'].'</a></li></br>';
				}
			}
			?>
		</ul>
	</div>

	<div class="keeptogether">
		<h3>Tournois terminés</h3>
		<ul>
			<?php
			foreach ($list as $tournoi){
				if($tournoi != '.' && $tournoi != '..'){
					$content = file_get_contents("json/Tournois/$tournoi");
					$json = json_decode($content, true);
					if($json['Etat'] == 5)
						echo '<li><a href="' . BASEURL . '/index.php/home/inscription/'.$json['Ref'].'">'.$json['Nom'].'</a></li></br>';
				}
			}
			?>
		</ul>
	</div>
</div>