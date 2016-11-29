![CCL-logo](sachem/Images/logo-cegepBLANC.jpg)![sachem-logo](/sachem/Images/logo-sachemBLANC.jpg)

# Projet web du SACHEM

* [SACHEM](#sachem)
    * [Processus](#processus)
    * [Inscription](#inscription)
    * [Jumelage](#jumelage)
    * [Après jumelage](#après-jumelage)
* [Développement](#développement)
    * [Matériels nécessaires](#matériels-nécessaires)
    * [Dépendances](#dépendances)
    * [Cloner et commencer](#cloner-et-commencer)
        * [Web.config](#webconfig)
    * [Modèle de branches](#modèle-de-branches)
    * [Issues](#issues)
* [Captures](#captures)
* [Crédits](#crédits)
   
# SACHEM

Projet ASP.NET MVC 5 pour informatiser les documents d'inscription et de jumelage entre élèves du service d'aide en mathématiques au Cégep de Lévis-Lauzon.

## Processus

Le SACHEM permet à un étudiant (élève aidé) de réclamer de l'aide pour améliorer sa compréhension de différentes notions en mathématiques dans le but d'améliorer ses résultats. Le SACHEM permet également à un étudiant ayant de la facilité en mathétiques d'accompagner un élève en difficulté dans sa compréhension des mathématiques, de devenir un tuteur : Tuteur de cours (cours de Tutorat en Mathématiques à son horaire) ou tuteur bénévole (moyenne générale de plus de 85%). Par la suite, selon le bassin des tuteurs disponibles et le nombre maximale de 4 jumelages par tuteur, le SACHEM mettera en place des jumelages entre élève aidé et tuteur selon leur propre horaire de disponibilités. Chaque jumelage sera supervisé par un enseignant pour assurer un suivi constant de la progression de l'élève aidé.

## Inscription

Ce service est disponible que pour les étudiants ayant un cours mathématiques durant la session actuelle au Cégep de Lévis-Lauzon. L'étudiant doit définir quel type de compte il désire créer: étudiant aidé, étudiant tuteur bénévole, étudiant tuteur de cours. Puis, il doit remplir le formualire d'inscription.

## Jumelage

Les jumelages seront créés une fois la période d'inscription terminée. Ils seront créés de façon automatique selon la compatibilité des plages de disponibilités des étudiants.


## Après jumelage

Par l'entremise du site, l'enseignant doit livrer des documents (devoirs ou instructions) au tuteur d'un jumelage, pour être complétés avec son étudiant aidé. Le tuteur doit compléter son cahier de suivi hebdomadairement pour indiquer la progression de l'étudiant aidé. L'enseignant peut annoter ce cahier de suivi et le retourner au tuteur par la suite.

# Développement

## Matériels nécessaires

* [Git](http://git-scm.com/book/en/v2/Getting-Started-Installing-Git): Versionner sur GitHub le projet
* [Visual Studio 2015](https://www.visualstudio.com/post-download-vs/?sku=community&clcid=0x409&telem=ga): Programmer sous ASP.NET MVC
* [.Net Framework 4.6](https://www.microsoft.com/en-ca/download/details.aspx?id=48130): Bibliothèque .Net
* [SQL Server 2016](https://www.microsoft.com/en-us/sql-server/sql-server-downloads): Base de données
* [SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx): Administration de la base de données
* [BD Backup](https://github.com/dinfcll/sachem/blob/master/sachem/BD_Presentation.bak): Fichier de la base de données de base.

## Dépendances

Le projet contient certaines dépendances à des paquets NuGet:
- ASP.Net
- ASP.Net MVC
- JQuery
- Bootstrap
- Dropzone

## Cloner et commencer

Une fois SQL Server installé et la base de données SACHEM (BD Backup) ajouté par l'interface de SQL Server Management Studio, vous êtes prêt à cloner le projet. Soit par Visual Studio dans la section `Team Explorer` ou par ligne de commande Git:

`git clone https://github.com/dinfcll/sachem.git`

Une fois chargé, la première manipulation à faire est de s'assurer que votre connexion à la BD de SACHEM correspond bien à celle de votre ordinateur dans le fichier `web.config`.

### Web.config

Vous aurez à modifier le serveur de connexion si vous avez configuré un usager SQL Server. Vous pouvez le constater lorsque vous utilisez SQL Management Server, le "Server name", exemple `localhost\SQL`, que vous utilisez est le même que dans ce fichier. Modifier `localhost` par `localhost\MON_USAGER_SQL_SERVER`, s'il y a lieu.

```sh
 <connectionStrings>
    <add name="SACHEMEntities" connectionString="....data source=localhost;...." />
  </connectionStrings>
```

Veiller à ne pas envoyer sur la branche principale `master` votre `web.config` personnalisé. Retirez le dans le dernier `commit` lors de votre `pull request`.

## Modèle de branches

Notre branche principale est `master`.

Toute nouvelle fonctionnalité, correction de bug et/ou test doit être réalisé dans une `nouvelle branche` ou avoir fait un `fork` du projet. Une fois votre changement réalisé et prêt, une demande de `Pull Request` peut être créé pour affecter `master`.

Nous demandons à ceux travaillant sur des branches autres que `master` de synchroniser fréquemment avec `master` pour obtenir les plus récents ajouts.

Veiller à ne pas envoyer sur la branche principale `master` votre `web.config` personnalisé. Retirez le dans le dernier `commit` lors de votre `pull request`.

## Issues

Rapportez en tant que nouvelle `issue` tout:
- Bug à fixer
- Nouvelle fonctionnalité à concevoir
- Suggestion ou question

Ajoutez votre nouvelle issue dans `Projects` > `Projet SACHEM`:

Par le menu `Add cards`, sélectionnez votre issue et déposez le dans `A faire`.

Un suivi de votre issue sera fait.

# Captures

<img width="150" src="https://git.dinf.cll.qc.ca/lainessej/sachem/uploads/95b7a5e0607519d675b55dbb57844558/Inscription_1.PNG"/>
<img width="150" src="https://git.dinf.cll.qc.ca/lainessej/sachem/uploads/72f685881b3f055fea8f958903fb0ab4/MAJ_jumelage_-_d%C3%A9tail_-_jumelage_possible.PNG"/>
<img width="150" src="https://git.dinf.cll.qc.ca/lainessej/sachem/uploads/39d0284b0e52e0551d1baf6bbfbd6da0/rapport_initial_B.PNG"/>

# Crédits

Merci à notre équipe de concepteurs principaux, en ordre alphabétique:

[Robert Ableson](#),
[Pierre Bégin](#),
[Anthony Benoit-Caron](https://github.com/Anthobc),
[Marie-Christine Boilard](#),
[Simon Huard](https://github.com/simHuard),
[Olivier Lafleur](https://github.com/olafleur),
[Josée Lainesse](#),
[Alexys Leclerc](https://github.com/LeclercA),
[Dylan Marcotte](https://github.com/FragZServer),
[Alexandre Martineau](https://github.com/AlexandreMartineau),
[Jose Ouellet](https://github.com/jwallet),
[Guillaume Prud'homme](https://github.com/GuillaumePrudhomme),
[Louis-Roch Tessier](https://github.com/louisrochtessier),
[Loïc Turgeon](https://github.com/loicturgeon),
[Alexandre Venables](https://github.com/VenablesAu),
[Cristian Zubieta](https://github.com/cristianzubieta),
et nos supers [contribueurs](https://github.com/dinfcll/sachem/graphs/contributors).
