struct Foo(String);

struct Bar(String);

impl From<Foo> for Bar {
    fn from(f: Foo) -> Self {
        let Foo(s) = f;
        Bar(s)
    }
}

impl From<Bar> for String {
    fn from(b: Bar) -> Self {
        let Bar(s) = b;
        s
    }
}

fn main() {
    let foo: Foo = Foo("Hello, world!".to_string());
    println!("{}", String::from(Bar::from(foo)));
}
