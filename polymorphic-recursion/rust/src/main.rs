enum IntList {
    Nil,
    Cons(i64, Box<IntList>),
}

impl IntList {
    pub fn length(self) -> i64 {
        match self {
            IntList::Nil => 0,
            IntList::Cons(_, t) => 1 + t.length(),
        }
    }
}

fn main() {
    let int_list = IntList::Cons(
        0,
        Box::new(IntList::Cons(
            1,
            Box::new(IntList::Cons(
                2,
                Box::new(IntList::Cons(3, Box::new(IntList::Nil))),
            )),
        )),
    );
    println!("{}", int_list.length());
}
