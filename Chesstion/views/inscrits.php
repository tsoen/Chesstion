
<style type=text/css>
	.papi_liste_c {font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000000; background-color: #FFFFFF;}
	.papi_liste_f {font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000000; background-color: #E0E0E0;}
	.papi_small_c {font-family: Arial, Helvetica, sans-serif; font-size: 10px; font-weight: normal; color: #000000; background-color: #FFFFFF;}
	.papi_small_f {font-family: Arial, Helvetica, sans-serif; font-size: 10px; font-weight: normal; color: #000000; background-color: #E0E0E0;}
	.papi_small_t {font-family: Arial, Helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #000000; background-color: #FFFFFF;}
	.papi_liste_t {font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; background-color: #FFFFFF;}
	.papi_titre {font-family: Arial, Helvetica, sans-serif; font-size: 18px; font-weight: bold; color: #000000; margin-bottom: 12px; border: thin none; background-color: #FFFFFF;}
	.papi_titre_big {font-family: Arial, Helvetica, sans-serif; font-size: 24pt; font-weight: bold;}
	.papi_titre_medium {font-family: Arial, Helvetica, sans-serif; font-size: 16pt; font-weight: bold;}
	.papi_l {border-style: solid; border-color: black; border-width:0; text-align:left;}
	.papi_r {border-style: solid; border-color: black; border-width:0; text-align:right;}
	.papi_c {border-style: solid; border-color: black; border-width:0; text-align:center;}
	.papi_border_l {border-style: solid; border-color: black; border-width:1px; text-align:left;}
	.papi_border_r {border-style: solid; border-color: black; border-width:1px; text-align:right;}
	.papi_border_c {border-style: solid; border-color: black; border-width:1px; text-align:center;}

    table{
        /*border-spacing: 20px;*/
        border-collapse: separate;
    }

    table tr.papi_titre td {
        padding-top: 1em;
        padding-bottom: 1em;
    }

</style>

<div align=center>
	<table width=750 cellpadding="10" cellspacing="10" >
		<tr class=papi_titre>
			<td colspan=8 align=center><?=$tournoi['Nom']?></td>
		</tr>
        <tr class=papi_titre>
            <td colspan=8 align=center>Liste des participants</td>
        </tr>
		<?php
		$num = 1;
		foreach ($tournoi['Opens'] as $t){
			echo '<tr class=papi_titre>
				<td colspan=8 align=center>Open '.$t['Nom'].'</td>
			</tr>
			<tr class=papi_liste_t>
				<td class=papi_r>Nr</td>
				<td class=papi_r>&nbsp;</td>
				<td class=papi_l>Nom</td>
				<td class=papi_c>Elo</td>
				<td class=papi_c>Cat.</td>
				<td class=papi_c>Fede</td>
				<td class=papi_c>Ligue</td>
				<td class=papi_l>Open</td>
			</tr>';

			if($inscrits != null) {
				usort($inscrits, "cmp");
				$couleur = 1;
				foreach ($inscrits as $i) {
					if ($i['OpenRef'] == $t['Ref']) {
						$ligue = Joueur::getLigueClub($i['ClubRef']);
						if ($couleur % 2 == 1) {
							echo '<tr class=papi_liste_f>';
						} else {
							echo '<tr class=papi_liste_c>';
						}
						echo '<td class=papi_r>' . $num . '</td>
						<td class=papi_r>' . $i['FideTitre'] . '&nbsp;</td>
						<td class=papi_l>' . $i['Nom'] . ' ' . $i['Prenom'] . '</td>
						<td class=papi_c>' . $i['Elo'] . '&nbsp;' . $i['Fide'] . '</td>
						<td class=papi_c>' . $i['Cat'] . '</td>
						<td class=papi_c>' . $i['Federation'] . '</td>
						<td class=papi_c>' . $ligue . '</td>
						<td class=papi_l>' . $t['Nom'] . '</td>
					</tr>';
						$num++;
						$couleur++;
					}
				}
			}
		}?>
	</table>
</div>