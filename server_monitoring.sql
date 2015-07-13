-- phpMyAdmin SQL Dump
-- version 4.4.10
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Lun 13 Juillet 2015 à 12:11
-- Version du serveur :  5.6.21-log
-- Version de PHP :  5.6.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `server_monitoring`
--

-- --------------------------------------------------------

--
-- Structure de la table `BaseDonnee`
--

CREATE TABLE IF NOT EXISTS `BaseDonnee` (
  `id` int(11) NOT NULL,
  `idProjet` int(11) NOT NULL,
  `host` varchar(30) NOT NULL,
  `databaseName` varchar(50) NOT NULL,
  `user` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL,
  `cheminSauvegarde` text NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `EnumTheme`
--

CREATE TABLE IF NOT EXISTS `EnumTheme` (
  `id` int(11) NOT NULL,
  `libelle` varchar(20) NOT NULL,
  `cssClass` varchar(20) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `EnumTheme`
--

INSERT INTO `EnumTheme` (`id`, `libelle`, `cssClass`) VALUES
(1, 'Bleu', 'blue'),
(2, 'Orange', 'orange'),
(3, 'Rouge', 'red'),
(4, 'Violet', 'purple'),
(5, 'Gris', 'grey'),
(6, 'Vert', 'green');

-- --------------------------------------------------------

--
-- Structure de la table `Log`
--

CREATE TABLE IF NOT EXISTS `Log` (
  `id` int(11) NOT NULL,
  `idProjet` int(11) NOT NULL,
  `libelle` varchar(50) NOT NULL,
  `cheminFichier` text NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `Projet`
--

CREATE TABLE IF NOT EXISTS `Projet` (
  `id` int(11) NOT NULL,
  `libelle` varchar(50) NOT NULL,
  `couleur` varchar(20) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Projet`
--

INSERT INTO `Projet` (`id`, `libelle`, `couleur`) VALUES
(1, 'Activa', '1'),
(2, 'iZyFrais', '2'),
(3, 'iFaxnet', '3'),
(4, 'IDDIC', '6'),
(5, 'iWi', '5');

-- --------------------------------------------------------

--
-- Structure de la table `Serveur`
--

CREATE TABLE IF NOT EXISTS `Serveur` (
  `id` int(11) NOT NULL,
  `libelle` varchar(50) NOT NULL,
  `ipLocale` varchar(20) NOT NULL,
  `ipPublique` varchar(20) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Serveur`
--

INSERT INTO `Serveur` (`id`, `libelle`, `ipLocale`, `ipPublique`) VALUES
(1, 'MARS', '172.15.19.56', '82.25.156.23');

-- --------------------------------------------------------

--
-- Structure de la table `Utilisateur`
--

CREATE TABLE IF NOT EXISTS `Utilisateur` (
  `id` int(11) NOT NULL,
  `nom` varchar(50) NOT NULL,
  `login` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `Utilisateur`
--

INSERT INTO `Utilisateur` (`id`, `nom`, `login`, `password`) VALUES
(1, 'Sylvain Lemoine', 's.lemoine@izysolutions.com', 'da0f6cfe6511cf968b5fb1dcd50236b303f78cf2');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `BaseDonnee`
--
ALTER TABLE `BaseDonnee`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `EnumTheme`
--
ALTER TABLE `EnumTheme`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `Log`
--
ALTER TABLE `Log`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `Projet`
--
ALTER TABLE `Projet`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `Serveur`
--
ALTER TABLE `Serveur`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `Utilisateur`
--
ALTER TABLE `Utilisateur`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `BaseDonnee`
--
ALTER TABLE `BaseDonnee`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT pour la table `EnumTheme`
--
ALTER TABLE `EnumTheme`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT pour la table `Log`
--
ALTER TABLE `Log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT pour la table `Projet`
--
ALTER TABLE `Projet`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT pour la table `Serveur`
--
ALTER TABLE `Serveur`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT pour la table `Utilisateur`
--
ALTER TABLE `Utilisateur`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
