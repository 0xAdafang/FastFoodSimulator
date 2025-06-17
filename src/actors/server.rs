use std::{sync::{Arc, Mutex}, time::Duration};
use tokio::time::sleep;

use crate::models::order::Order;

pub struct Server {
    pub id: usize,
    pub delivery_time: Duration,
}

impl Server {
    
    pub fn new(id: usize, delivery_secs: u64) -> Self {
        Self {
            id,
            delivery_time: Duration::from_secs(delivery_secs),
        }
    }

    pub async fn run(
        &self,
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
                println!("🧍‍♂️ Serveur {} prend la commande #{}", self.id, order.id);
                sleep(self.delivery_time).await;
                {
                    let mut served = served_orders.lock().unwrap();
                    served.push(order.clone());
                }
                println!("🍽️ Commande #{} servie au client par le serveur {}", order.id, self.id);
            } else {
                sleep(Duration::from_millis(100)).await;
            }
        }
    }
}