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
	out := make(chan struct{idx, data int})
	log := make(chan string)
	for i := 0; i < 100; i++ {
		go func(i int) {
                        out <- struct{idx, data int} {i, FetchData(i, log)}
		}(i)
	}
	var res struct{result[100]int; log string}
	for i := 0; i < 200; i++ {
		select {
		case d := <-out:
			res.result[d.idx] = d.data
		case msg := <-log:
			res.log += msg
		}
	}
	return res
}
