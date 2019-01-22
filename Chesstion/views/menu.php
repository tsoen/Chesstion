<?php
if($_SESSION['output'] != 1)
{
	if($_SESSION['ref'] == -1) {
		?>
		<nav class="navbar navbar-full navbar-default bg-faded">
			<div class="container-fluid">
				<div class="navbar-header">
					<h3 class="navbar-text text-primary">Cercle d'Echecs de la Thur</h3>
				</div>
				<ul class="nav navbar-nav" id="navbar">
					<li class="nav-item active" id="accueil">
						<a class="nav-link" href="<?php echo BASEURL ?>/index.php">Accueil</a>
					</li>
				</ul>
			</div>
		</nav>
	<?php
	} else {
	?>
		<nav class="navbar navbar-full navbar-default bg-faded">
			<div class="container-fluid">
				<div class="navbar-header">
					<h3 class="navbar-text text-primary"><?=$tournoi['Nom']?></h3>
				</div>
				<ul class="nav navbar-nav" id="navbar">
					<li class="nav-item" id="accueil">
						<a class="nav-link" href="<?php echo BASEURL ?>/index.php">Accueil</a>
					</li>
					<li class="nav-item active" id="inscription" onclick="return getOutput('inscription');">
						<a class="nav-link" href="">Inscription</a>
					</li>
					<li class="nav-item" id="inscrits" onclick="return getOutput('inscrits');">
						<a class="nav-link" href="">Inscrits</a>
					</li>
					<?php
					if($tournoi['Etat'] > 2) {
					?>
						<li class="nav-item" id="resultats" onclick="return getOutput('resultats');">
							<a class="nav-link" href="">Résultats</a>
						</li>
					<?php
					}
					?>
				</ul>
			</div>
		</nav>
		<?php
	}

	$_SESSION['output'] = 1;
}

?>