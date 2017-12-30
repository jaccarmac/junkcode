type
  IntListKind = enum
    ilNil,
    ilCons,
  IntList = ref object
    case kind: IntListKind
    of ilNil: nil
    of ilCons:
      head: int
      tail: IntList
  Nested[T] = ref object of RootObj
  NestedEpsilon[T] = ref object of Nested[T]
  NestedCons[T] = ref object of Nested[T]
    head: T
    tail: Nested[seq[T]]

proc length(il: IntList): int =
  case il.kind
  of ilNil: 0
  of ilCons: 1 + il.tail.length

method length[T](n: Nested[T]): int {.base.} = quit "override!"

method length[T](n: NestedEpsilon[T]): int = 0

method length[T](n: NestedCons[T]): int = 1 + n.tail.length

let test_il = IntList(kind: ilCons, head: 0, tail: IntList(kind: ilCons, head: 1, tail: IntList(kind: ilNil)))
echo test_il.length

let test_nest = NestedCons[int](head: 0, tail: NestedCons[seq[int]](head: @[1, 2, 3], tail: NestedCons[seq[seq[int]]](head: @[@[1, 2, 3]], tail: NestedEpsilon[seq[seq[seq[int]]]]())))
echo test_nest.length
