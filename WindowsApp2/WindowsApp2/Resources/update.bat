<?php
session_start();

			
		
			$anwender_name=$_SESSION['anwender_name'];
			$id_anwender=$_SESSION['id_anwender'];
			$test = $_GET['like'];
			
				//Eine DB-Verbindung wird hergestellt
				$mysqli = new mysqli($host, $user, $pwd, $db);

				if ($mysqli->connect_errno)
				{
					echo "Anmeldung fehlgeschlagen: ". $mysqli->connect_errno;
					exit(0);
				}
				//Der Zeichensatz zur Verstaendigung mit der DB wird festgelegt
				$mysqli->set_charset("utf8");
				
				$select_anweisung = "SELECT COUNT(*) AS anzahl
									 FROM tbl_anwender_rezepte_like
									 WHERE id_anwender = $id_anwender
									 AND   rezepte_id  = $test"; 
									 
									 $ergebnismenge = $mysqli->query($select_anweisung);
									 $datensatz = $ergebnismenge->fetch_assoc();
									 if($datensatz['anzahl'] == 0)
									 {
										 $insert_anweisung = "INSERT INTO tbl_anwender_rezepte_like
									(rezepte_id, id_anwender, datum)
									VALUES ( '".$test."', '".$id_anwender."', CURDATE() ) ";

									//Die SELECT-Anweisung holt sich von der DB die Informationen in die Ergebnismenge
									$mysqli->query($insert_anweisung);
									 }
									 
				
				
				
				header ('location: rezepte_auswahl_anfrage.php');
				
	?>