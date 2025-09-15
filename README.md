# 🏆 Jeu d'Échecs en C# - Projet POO

Un jeu d'échecs complet développé en C# pour démontrer les concepts de la Programmation Orientée Objet.

## 🎯 Objectifs du Projet

Ce projet vise à démontrer la maîtrise des concepts fondamentaux de la POO :
- **Héritage** : Classes spécialisées héritant de `Piece`
- **Polymorphisme** : Méthodes virtuelles et abstraites
- **Encapsulation** : Propriétés privées avec accesseurs
- **Abstraction** : Classe abstraite `Piece`
- **Composition** : `Echiquier` contient des `Piece`

## 🚀 Fonctionnalités

### ✅ Itération 1 - Fondations (TERMINÉE)
- [x] Classe `Position` avec validation et notation algébrique
- [x] Classe abstraite `Piece` avec méthodes virtuelles
- [x] Classe `Echiquier` pour la gestion du plateau
- [x] Enumération `Couleur` pour les pièces
- [x] Tests unitaires intégrés

### 🔄 Itération 2 - Pièces (EN COURS)
- [ ] Implémentation de toutes les pièces d'échecs
- [ ] Règles de déplacement spécifiques à chaque pièce
- [ ] Validation des mouvements

### 📋 Itérations Prévues
- [ ] **Itération 3** : Système d'affichage du plateau
- [ ] **Itération 4** : Interface de saisie des mouvements
- [ ] **Itération 5** : Validation complète (échec, échec et mat)
- [ ] **Itération 6** : Boucle principale du jeu
- [ ] **Itération 7** : Règles avancées (roque, en passant, promotion)

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
