
<?php
if($list == null || count($list) == 2)
	echo "<p style='margin-left: 20px;'>Aucun résultat n'a été publié pour le moment.";
else {
	echo '<ul>';

	echo '<li>Appariements : <select name="appariements" id="appariements">';
	foreach ($list as $fichier) {
		if ($fichier != '.' && $fichier != '..' && preg_match("#^Appariements de la ronde #", $fichier)) {
			echo '<option>' . substr($fichier, 0, $fichier . ob_get_length() - 5) . '</option>';
		}
	}
	echo '</select>
		<button onclick="papiHtml(' . "appariements" . ')">Go</button>
		</li><br/>';

	echo '<li>Résultats des rondes : <select name="resRonde" id="resRonde">';
	foreach ($list as $fichier) {
		if ($fichier != '.' && $fichier != '..' && preg_match("#^Résultats de la ronde #", $fichier)) {
			echo '<option>' . substr($fichier, 0, $fichier . ob_get_length() - 5) . '</option>';
		}
	}
	echo '</select>
		<button onclick="papiHtml(' . "resRonde" . ')">Go</button>
		</li><br/>';

	echo '<li>Classements : <select name="classements" id="classements">';
	foreach ($list as $fichier) {
		if ($fichier != '.' && $fichier != '..' && preg_match("#^Classement après la ronde #", $fichier)) {
			echo '<option>' . substr($fichier, 0, $fichier . ob_get_length() - 5) . '</option>';
		}
	}
	echo '</select>
		<button onclick="papiHtml(' . "classements" . ')">Go</button>
		</li><br/>';

	echo '<li>Grilles américaines : <select name="grilles" id="grilles">';
	foreach ($list as $fichier) {
		if ($fichier != '.' && $fichier != '..' && preg_match("#^Grille américaine après la ronde #", $fichier)) {
			echo '<option>' . substr($fichier, 0, $fichier . ob_get_length() - 5) . '</option>';
		}
	}
	echo '</select>
		<button onclick="papiHtml(' . "grilles" . ')">Go</button>
		</li><br/>';

	echo '</ul>';
}
