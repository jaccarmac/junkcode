(define binary
  0 -> "0"
  1 -> "1"
  N -> (cn (binary (clr.unbox (clr.int (/ N 2))))
           (str (clr.unbox (clr.invoke-static "System.Decimal" "Remainder" [(clr.decimal N) (clr.decimal 2)])))))
