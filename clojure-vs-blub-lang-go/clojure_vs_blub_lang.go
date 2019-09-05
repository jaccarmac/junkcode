package clojure_vs_blub_lang

import (
	"fmt"
	"sync"
	"time"
)

func FetchData(idx int, n int, out chan struct{idx, data int}, log chan string) {
	time.Sleep(time.Second)
	log <- fmt.Sprintf("Fetched record: %d", n)
	out <- struct{idx, data int} {idx, n}
}

func GetData(log chan string) []int {
	var wg sync.WaitGroup
	wg.Add(100)
	out := make(chan struct{idx, data int}, 100)
	for i := 0; i < 100; i++ {
		go func(i int) {
			defer wg.Done()
			FetchData(i, i, out, log)
		}(i)
	}
	wg.Wait()
	close(out)
	res := make([]int, 100)
	for d := range out {
		res[d.idx] = d.data
	}
	return res
}
