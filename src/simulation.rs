use std::sync::{Arc, Mutex};
use tokio::task;

use crate::actors::customer::Customer;
use crate::actors::{cook::Cook, order_taker::OrderTaker, server::Server};
use crate::models::order::Order;
use crate::utils::timer::{ORDER_TAKER_INTERVAL, COOK_PREPARATION_TIME, SERVER_DELIVERY_TIME};


pub async fn run_simulation() {

    let waiting_orders = Arc::new(Mutex::new(Vec::<Order>::new()));
    let ready_orders = Arc::new(Mutex::new(Vec::<Order>::new()));
    let served_orders = Arc::new(Mutex::new(Vec::<Order>::new()));
    let order_counter = Arc::new(Mutex::new(0));

    let order_taker = OrderTaker::new(1, ORDER_TAKER_INTERVAL.as_secs());
    let cook = Cook::new(1, COOK_PREPARATION_TIME.as_secs());
    let server = Server::new(1, SERVER_DELIVERY_TIME.as_secs());

    {
        let w = Arc::clone(&waiting_orders);
        let c = Arc::clone(&order_counter);
        task::spawn(async move {
            order_taker.run(w,c).await;
        });
    }

    {
        let w = Arc::clone(&waiting_orders);
        let r = Arc::clone(&ready_orders);
        task::spawn(async move {
            cook.run(w, r).await;
        });
    }

    {

        let r = Arc::clone(&ready_orders);
        let s = Arc::clone(&served_orders);
        tokio::spawn(async move {
            server.run(r, s).await;
        });
    }

    {
        let r = Arc::clone(&ready_orders);
        let s = Arc::clone(&served_orders);
        task::spawn(async move {
            Customer::run(r, s).await;
        });
    }

    loop {
        tokio::time::sleep(std::time::Duration::from_secs(5)).await;
        let served = served_orders.lock().unwrap();
        println!("📦 Commandes servies jusque là : {:?}", served.iter().map(|o| o.id).collect::<Vec<_>>());
    }
}