(define binary
  0 -> "0"
  1 -> "1"
  N -> (cn (binary (clr.unbox (clr.int (/ N 2))))
           (str (shen.mod N 2))))
