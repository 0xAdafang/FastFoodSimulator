mod actors;
mod models;
mod utils;
mod simulation;

#[tokio::main]
async fn main(){
    println!("🍟 Bienvenue dans FastFoodSimulator en Rust !");
    println!("⏳ Démarrage de la simulation...\n");

    simulation::run_simulation().await;
}
