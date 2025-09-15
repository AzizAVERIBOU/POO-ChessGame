# Jeu d'Échecs en C# - Projet POO

Un jeu d'échecs realiser sur console lors du cours de POO (programmation orientee objet) développé en C# pour démontrer les concepts de la Programmation Orientée Objet.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![C#](https://img.shields.io/badge/C%23-12.0-purple.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## Objectifs du Projet

Ce projet vise à démontrer la maîtrise des concepts fondamentaux de la POO :
- **Héritage** : Classes spécialisées héritant de `Piece`
- **Polymorphisme** : Méthodes virtuelles et abstraites
- **Encapsulation** : Propriétés privées avec accesseurs
- **Abstraction** : Classe abstraite `Piece`
- **Composition** : `Echiquier` contient des `Piece`

## Fonctionnalités

### ✅ Fonctionnalités Implémentées
- [x] **Toutes les pièces d'échecs** : Pion, Tour, Cavalier, Fou, Dame, Roi
- [x] **Règles de déplacement** : Mouvements spécifiques à chaque pièce
- [x] **Interface utilisateur** : Menu interactif en console
- [x] **Affichage amélioré** : Indication visuelle des pièces du joueur actuel
- [x] **Validation des mouvements** : Vérification des règles d'échecs
- [x] **Notation algébrique** : Support des mouvements en format "e2-e4"
- [x] **Système de tours** : Gestion des joueurs blancs et noirs
- [x] **Détection d'échec** : Vérification basique de l'échec

## Architecture

```
echec-poo/
├── Models/
│   ├── Position.cs      # Gestion des coordonnées et notation algébrique
│   ├── Couleur.cs       # Enumération des couleurs
│   └── Piece.cs         # Classe abstraite de base
├── Game/
│   ├── Echiquier.cs     # Gestion du plateau et affichage
│   ├── JeuEchecs.cs     # Logique principale du jeu
│   ├── Joueur.cs        # Représentation des joueurs
│   └── InterfaceJeu.cs  # Interface utilisateur
├── Pieces/
│   ├── Pion.cs          # Implémentation du pion
│   ├── Tour.cs          # Implémentation de la tour
│   ├── Cavalier.cs      # Implémentation du cavalier
│   ├── Fou.cs           # Implémentation du fou
│   ├── Dame.cs          # Implémentation de la dame
│   └── Roi.cs           # Implémentation du roi
└── Program.cs           # Point d'entrée et tests
```

## Utilisation

### Prérequis
- [.NET 8.0](https://dotnet.microsoft.com/download) ou version ultérieure

### Installation et Exécution

```bash
# Cloner le dépôt
git clone https://github.com/VOTRE_USERNAME/jeu-echecs-csharp.git
cd jeu-echecs-csharp

# Compiler le projet
dotnet build

# Exécuter le jeu
dotnet run
```

### Comment Jouer

1. **Lancer le jeu** : Exécutez `dotnet run`
2. **Afficher l'échiquier** : Choisissez l'option 2
3. **Jouer un coup** : Choisissez l'option 3 et entrez votre mouvement (ex: "e2-e4")
4. **Voir les mouvements** : Choisissez l'option 4 pour voir les mouvements possibles d'une pièce

## Fonctionnalités Spéciales

### Indication Visuelle
- Les pièces du joueur actuel sont entourées d'astérisques (*P*)
- Interface claire avec légende explicative

### Notation Algébrique
- Support complet de la notation standard (e2-e4, a7a5, etc.)
- Validation automatique des mouvements

### Architecture POO
- **Héritage** : Chaque pièce hérite de la classe `Piece`
- **Polymorphisme** : Méthodes virtuelles redéfinies pour chaque pièce
- **Encapsulation** : Propriétés privées avec accesseurs publics
- **Abstraction** : Classe `Piece` abstraite avec méthodes abstraites
- **Composition** : `Echiquier` contient un tableau de `Piece`

## Tests

Le projet inclut des tests intégrés qui s'exécutent automatiquement au lancement :
- Test de l'initialisation de l'échiquier
- Test des mouvements de base
- Test de la validation des positions

## Améliorations Futures

- [ ] Prise en passant
- [ ] Promotion des pions
- [ ] Roque (petit et grand)
- [ ] Interface graphique
- [ ] Sauvegarde/chargement de parties

## Licence

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.

---

*Développé dans le cadre d'un projet d'apprentissage de la Programmation Orientée Objet en C#*
