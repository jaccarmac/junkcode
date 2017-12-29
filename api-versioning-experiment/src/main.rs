#![feature(custom_derive)]
#![feature(plugin)]
#![plugin(rocket_codegen)]

extern crate rocket;
extern crate rocket_contrib;
#[macro_use]
extern crate serde_derive;

mod index;
mod sum;
mod versioning;

fn main() {
    rocket::ignite()
        .mount("/", routes![index::index, sum::sum])
        .launch();
}
