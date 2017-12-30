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

enum Nested<T> {
    Epsilon,
    Cons(T, Box<Nested<Vec<T>>>),
}

impl<T> Nested<T> {
    pub fn length(self) -> i64 {
        match self {
            Nested::Epsilon => 0,
            Nested::Cons(_, t) => 1 + t.length(),
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

    let nested = Nested::Cons(
        0,
        Box::new(Nested::Cons(vec![1, 2, 3], Box::new(Nested::Epsilon))),
    );
    println!("{}", nested.length());
}
