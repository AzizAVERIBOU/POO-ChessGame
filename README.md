# 🏆 Jeu d'Échecs en C# - Projet POO

Un jeu d'échecs complet développé en C# pour démontrer les concepts de la Programmation Orientée Objet.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![C#](https://img.shields.io/badge/C%23-12.0-purple.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## 🎯 Objectifs du Projet

Ce projet vise à démontrer la maîtrise des concepts fondamentaux de la POO :
- **Héritage** : Classes spécialisées héritant de `Piece`
- **Polymorphisme** : Méthodes virtuelles et abstraites
- **Encapsulation** : Propriétés privées avec accesseurs
- **Abstraction** : Classe abstraite `Piece`
- **Composition** : `Echiquier` contient des `Piece`

## 🚀 Fonctionnalités

### ✅ Fonctionnalités Implémentées
- [x] **Toutes les pièces d'échecs** : Pion, Tour, Cavalier, Fou, Dame, Roi
- [x] **Règles de déplacement** : Mouvements spécifiques à chaque pièce
- [x] **Interface utilisateur** : Menu interactif en console
- [x] **Affichage amélioré** : Indication visuelle des pièces du joueur actuel
- [x] **Validation des mouvements** : Vérification des règles d'échecs
- [x] **Notation algébrique** : Support des mouvements en format "e2-e4"
- [x] **Système de tours** : Gestion des joueurs blancs et noirs
- [x] **Détection d'échec** : Vérification basique de l'échec

## 🏗️ Architecture

```
echec-poo/
├── Models/
│   ├── Position.cs      # Gestion des coordonnées
│   ├── Couleur.cs       # Enumération des couleurs
│   └── Piece.cs         # Classe abstraite de base
├── Game/
│   └── Echiquier.cs     # Gestion du plateau
└── Program.cs           # Point d'entrée et tests
```

## 🎮 Utilisation

```bash
# Compilation
dotnet build

# Exécution
dotnet run
```

## 🧪 Tests

Le projet inclut des tests unitaires intégrés dans `Program.cs` pour valider le fonctionnement des classes de base.

## 📚 Concepts POO Démonstrés

1. **Héritage** : Chaque pièce hérite de la classe `Piece`
2. **Polymorphisme** : Méthodes `PeutSeDeplacerVers()` et `ObtenirMouvementsPossibles()` redéfinies
3. **Encapsulation** : Propriétés privées avec accesseurs publics
4. **Abstraction** : Classe `Piece` abstraite avec méthodes abstraites
5. **Composition** : `Echiquier` contient un tableau de `Piece`

## 🚧 Développement

Ce projet suit une approche Agile avec des itérations courtes et des tests continus.

### Prochaine Étape
Implémentation des pièces d'échecs avec leurs règles de déplacement spécifiques.

---

*Développé dans le cadre d'un projet d'apprentissage de la Programmation Orientée Objet en C#*
