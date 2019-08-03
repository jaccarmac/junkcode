(ns extract
  (:require-macros [cljs.core.async.macros :refer [go]])
  (:require [cljs.core.async :refer [chan put! <!]]
            ["pdf.js-extract" :refer [PDFExtract]]))

(defn extract [fn]
  (let [data-chan (chan)]
    (.extract (new PDFExtract)
              fn
              #js{}
              #(do (if %1
                     (println %1)
                     (put! data-chan %2))))
    data-chan))

(defn compare-text-rects [r s]
  (compare [(:y r) (:x r)] [(:y s) (:x s)]))

(defn text-in-order [pdf]
  (->> (js->clj pdf :keywordize-keys true)
       :pages
       first
       :content
       (sort compare-text-rects)
       (map :str)))

(defn main [& cli-args]
  (go (println (text-in-order (<! (extract (first cli-args)))))))
