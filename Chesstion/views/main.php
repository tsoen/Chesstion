<!DOCTYPE html>
<html lang="fr">
<head>
    <title>Démo</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="description" content="Demo project">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
          integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
          integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
            integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
            crossorigin="anonymous"></script>
    <script src="http://www.w3schools.com/lib/w3data.js"></script>
    <script src="<?php echo BASEURL ?>/js/nav.js"></script>
    <script src="<?php echo BASEURL ?>/js/formAnimation.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="<?php echo BASEURL ?>/css/style.css">
    <script src='https://www.google.com/recaptcha/api.js'></script>
</head>

<body>

<?php
include 'menu.php';
?>

<main id="include">

    <div id="jsmessage"></div>

    <?php

    if (isset($_SESSION['message'])) {
        $m = $_SESSION['message'];
        echo('<div class="message ' . $m['type'] . '">' . $m['text'] . '</div>');

        unset($_SESSION['message']);
    }

    echo $content;
    ?>
</main>

</body>
</html>