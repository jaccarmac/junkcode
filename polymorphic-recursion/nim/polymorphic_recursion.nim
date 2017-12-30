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
  NestedKind = enum
    nEpsilon,
    nCons,
  Nested[T] = ref object
    case kind: NestedKind
    of nEpsilon: nil
    of nCons:
      head: T
      tail: Nested[seq[T]]

proc length(il: IntList): int =
  case il.kind
  of ilNil: 0
  of ilCons: 1 + il.tail.length

proc length(n: Nested): int =
  case n.kind
  of nEpsilon: 0
  of nCons: 1 + n.tail.length

let test_il = IntList(kind: ilCons, head: 0, tail: IntList(kind: ilCons, head: 1, tail: IntList(kind: ilNil)))
echo test_il.length

let test_nest = Nested[int](kind: nCons, head: 0, tail: Nested[seq[int]](kind: nCons, head: @[1, 2, 3], tail: Nested[seq[seq[int]]](kind: nEpsilon)))
echo test_nest.length
