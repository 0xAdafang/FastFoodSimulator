use std::{sync::{Arc, Mutex}, time::Duration};
use tokio::time::sleep;

use crate::models::order::Order;

pub struct Cook {
    pub id : usize,
    pub preparation_time: Duration,
}

impl Cook {

    pub fn new(id: usize, preparation_time_secs: u64) -> Self {
        Self {
            id, 
            preparation_time: Duration::from_secs(preparation_time_secs)
        }
    }

    pub async fn run(
        &self, 
        waiting_orders: Arc<Mutex<Vec<Order>>>,
        ready_orders: Arc<Mutex<Vec<Order>>>,

    ) {
        loop {
            let maybe_order = {
                let mut queue = waiting_orders.lock().unwrap();
                if !queue.is_empty() {
                    Some(queue.remove(0))
                } else {
                    None
                }
            };

            if let Some(order) = maybe_order {
                println!("👨‍🍳 Cook {} prépare la commande #{}", self.id, order.id);
                sleep(self.preparation_time).await;
                {
                    let mut ready = ready_orders.lock().unwrap();
                    ready.push(order.clone());
                } 

                println!("✅ Cook {} a terminé la commande #{}", self.id, order.id);
            } else {
                sleep(Duration::from_millis(100)).await;
            }
        }
    }
}