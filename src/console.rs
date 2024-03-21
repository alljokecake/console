#![allow(unused)]

#[derive(Debug)]
pub struct Box {
    pub background_color: String,
    pub text_color: ConsoleTextType,
    pub text_font: String,
    pub text_font_size: u8,
}

#[derive(Debug)]
pub struct InputBox(Box);

#[derive(Debug)]
pub struct ReportBox(Box);

impl Default for InputBox {
    fn default() -> Self {
        InputBox(Box {
            background_color: String::from("#6f3538 A 241"),
            text_color: ConsoleTextType::UserInput,
            text_font: String::from("Roboto Mono"),
            text_font_size: 8,
        })
    }
}

impl Default for ReportBox {
    fn default() -> Self {
        ReportBox(Box {
            background_color: String::from("#1f0707 A 254"),
            text_color: ConsoleTextType::Info,
            text_font: String::from("Roboto Mono"),
            text_font_size: 8,
        })
    }
}


#[derive(Debug)]
pub struct Console {
    pub is_open: bool,
    pub state: ConsoleState,
    pub report_box: ReportBox,
    pub input_box: InputBox,
}

impl Default for Console {
    fn default() -> Self {
        Console {
            is_open: false,
            state: ConsoleState::Closed,
            report_box: ReportBox::default(),
            input_box: InputBox::default(),
        }
    }
}

impl Console {
    pub fn print_self(&self) {
        println!("{:#?}", self);
    }
}

#[derive(Debug, PartialEq)]
pub enum ConsoleState {
    Closed,
    OpenSmall,
    OpenBig,
}


#[derive(Debug)]
pub enum ConsoleTextType {
    Info,
    Warning,
    Debug,
    UserInput,
}
