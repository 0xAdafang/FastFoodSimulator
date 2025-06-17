use std::{sync::{Arc, Mutex}, time::Duration};
use tokio::time::sleep;

use crate::models::order::Order;

pub struct Customer;

impl Customer {
    pub async fn run(
        ready_orders: Arc<Mutex<Vec<Order>>>,
        served_orders: Arc<Mutex<Vec<Order>>>,
    ) {
        loop {
            let maybe_order = {
                let mut ready = ready_orders.lock().unwrap();
                if !ready.is_empty() {
                    Some(ready.remove(0))
                } else {
                    None
                }
            };

            if let Some(order) = maybe_order {
                println!("🍽️ Le client a récupéré la commande #{}", order.id);
                let mut served = served_orders.lock().unwrap();
                served.push(order);
            } else {
                sleep(Duration::from_millis(100)).await;
            }
        }
    }
}