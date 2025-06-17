use std::{sync::{Arc, Mutex}, time::Duration};
use tokio::time::sleep;

use crate::models::order::Order;

pub struct OrderTaker {
    pub id: usize,
    pub interval: Duration,
}

impl OrderTaker {
    pub fn new(id: usize, interval_secs: u64) -> Self {
        Self {
            id,
            interval: Duration::from_secs(interval_secs),
        }
    }

    pub async fn run(
        &self,
        waiting_orders: Arc<Mutex<Vec<Order>>>,
        order_counter: Arc<Mutex<usize>>,
    ) {
        loop {
            let new_order = {
                let mut counter = order_counter.lock().unwrap();
                *counter += 1;
                Order {id: *counter}
            };

            {
                let mut queue = waiting_orders.lock().unwrap();
                queue.push(new_order.clone());
            }

            println!("📝 OrderTaker {} a pris la commande #{}", self.id, new_order.id);

            sleep(self.interval).await;
        }
    }
}