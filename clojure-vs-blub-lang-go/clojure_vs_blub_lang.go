package clojure_vs_blub_lang

import (
	"fmt"
	"time"
)

func FetchData(n int, log chan string) int {
	start := time.Now()
	for time.Now().Sub(start) < time.Second {}
	log <- fmt.Sprintf("Fetched record: %d\n", n)
        return n
}

func GetData() struct{result [100]int; log string} {
	var res struct{result[100]int; log string}
	log := make(chan string)
	for i := 0; i < 100; i++ {
		go func(i int) {
			res.result[i] = FetchData(i, log)
		}(i)
	}
	for i := 0; i < 100; i++ {
		res.log += <-log
	}
	return res
}
