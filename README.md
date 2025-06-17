🍔 FastFoodSimulator (Rust Edition)
FastFoodSimulator est une simulation console minimaliste d’un restaurant de type fast-food, réécrite en Rust. Le projet modélise le flux opérationnel de base : arrivée des clients, prise de commande, préparation en cuisine et service — chaque étape étant gérée asynchrone et de manière concurrente.

🎯 Ce projet m’a permis de pratiquer des concepts essentiels en Rust comme Arc, Mutex, tokio::sleep, la gestion de tâches asynchrones, ainsi que la séparation logique en modules.

✨ Fonctionnalités
⏱️ Simulation temps réel avec tokio::time

🧱 Workflow complet : OrderTaker → Cook → Server

🧵 Concurrence avec Arc<Mutex<>>

📦 Code modulaire, lisible, et extensible

🧠 Structure idéale pour expérimenter la logique asynchrone en Rust

🧩 Architecture
Composant	Rôle
OrderTaker	Génère une commande toutes les X secondes
Cook	Prépare les commandes en attente
Server	Délivre les commandes prêtes aux clients
Customer (optionnel)	Peut être simulé comme récupérant les commandes servies
Simulation	Lance et coordonne toutes les tâches
utils/timer.rs	Centralise les constantes temporelles
models/order.rs	Définit la structure Order

📦 Prérequis
Rust 1.70+

Cargo

VS Code ou tout autre IDE compatible Rust

🚀 Comment lancer
bash
Copier
Modifier
git clone https://github.com/0xAdafang/FastFoodSimulator.git
cd FastFoodSimulator
cargo run
🛠️ À venir (bonus potentiels)
Simulation sur durée limitée (ex : 60s)

Stats finales : commandes servies, temps moyen, etc.

Interface TUI avec ratatui

Export CSV ou JSON des commandes

🤝 Objectif
Ce projet a été réalisé dans le but d’améliorer ma maîtrise de Rust concurrentiel et de mieux comprendre le fonctionnement des threads sécurisés avec Arc/Mutex ainsi que les tasks asynchrones avec tokio. Il s'inscrit dans mon apprentissage continu du langage et me sert de base pour des projets plus avancés.
