mod console;
mod console_manager;

use std::io;
use console::Console;
use console::ConsoleState;

fn main() {
    draw_console();
}

fn draw_console() {
    check_event();
}

fn check_event() {
    let mut cereal = Console::default();
    cereal.print_self();
    loop {
        println!("Waiting for key: ");

        let mut input = String::new();
        io::stdin().read_line(&mut input).expect("Failed to read line");

        let trimmed_input = input.trim();


        // f1 & closed -> open_small
        // f1 & open_small -> closed
        // f1 & open_big -> closed 

        // shift+f1 & closed -> open_big 
        // shift+f1 & open_small -> open_big 
        // shift+f1 & open_big -> open_small 


        // under input_f1
        if trimmed_input == "f1" {
            match cereal.state {
                ConsoleState::Closed => {
                    cereal.state = ConsoleState::OpenSmall;
                    cereal.is_open = true;
                },
                ConsoleState::OpenSmall => {
                    cereal.state = ConsoleState::Closed;
                    cereal.is_open = false;
                },
                ConsoleState::OpenBig => {
                    cereal.state = ConsoleState::Closed;
                    cereal.is_open = false;
                },
            }
            cereal.print_self();

        // under input_shift_f1
        } else if trimmed_input == "shift+f1"{
            match cereal.state {
                ConsoleState::Closed => {
                    cereal.state = ConsoleState::OpenBig;
                    cereal.is_open = true;
                },
                ConsoleState::OpenSmall => {
                    cereal.state = ConsoleState::OpenBig;
                    cereal.is_open = true; // redundant?
                },
                ConsoleState::OpenBig => {
                    cereal.state = ConsoleState::OpenSmall;
                    cereal.is_open = true; // redundant?
                },
            }

            cereal.print_self();
        } else {
            break;
        }
    }
}
