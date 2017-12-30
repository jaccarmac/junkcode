# Package

version       = "0.1.0"
author        = "Jacob MacDonald"
description   = "See if Nim stack overflows for polymorphic recursion."
license       = "MIT"

# Dependencies

requires "nim >= 0.17.2"

# Tasks

task run, "Run the program.":
  --run
  setCommand "c", "polymorphic_recursion.nim"
