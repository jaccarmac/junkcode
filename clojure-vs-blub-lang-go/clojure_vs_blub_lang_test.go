package clojure_vs_blub_lang

import (
	"fmt"
	"testing"
)

func BenchmarkAddGigasecond(b *testing.B) {
	for i := 0; i < b.N; i++ {
		fmt.Printf("%v\n", GetData())
	}
}
