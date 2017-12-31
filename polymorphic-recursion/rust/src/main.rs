trait IntList {
    fn length(&self) -> i64;
}

struct IntListNil;
struct IntListCons(i64, Box<IntList>);

impl IntList for IntListNil {
    fn length(&self) -> i64 {
        0
    }
}

impl IntList for IntListCons {
    fn length(&self) -> i64 {
        1 + self.1.length()
    }
}

trait Nested<T> {
    fn length(&self) -> i64;
}

struct NestedEpsilon;
struct NestedCons<T>(T, Box<Nested<Vec<T>>>);

impl<T> Nested<T> for NestedEpsilon {
    fn length(&self) -> i64 {
        0
    }
}

impl<T> Nested<T> for NestedCons<T> {
    fn length(&self) -> i64 {
        1 + self.1.length()
    }
}

fn main() {
    let int_list = IntListCons(
        0,
        Box::new(IntListCons(
            1,
            Box::new(IntListCons(
                2,
                Box::new(IntListCons(3, Box::new(IntListNil))),
            )),
        )),
    );
    println!("{}", int_list.length());

    let nested = NestedCons::<i64>(
        0,
        Box::new(NestedCons::<Vec<i64>>(
            vec![1, 2],
            Box::new(NestedCons::<Vec<Vec<i64>>>(
                vec![vec![3], vec![4]],
                Box::new(NestedEpsilon),
            )),
        )),
    );
    println!("{}", nested.length());
}
