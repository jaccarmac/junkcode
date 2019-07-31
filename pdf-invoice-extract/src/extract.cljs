(ns extract
  (:require-macros [cljs.core.async.macros :refer [go]])
  (:require [cljs.core.async :refer [chan put! <!]]
            ["pdf.js-extract" :refer [PDFExtract]]))

(defn extract [fn]
  (go
    (let [data-chan (chan)]
      (.extract (new PDFExtract)
                fn
                #js{}
                #(do (if %1
                       (println %1)
                       (put! data-chan %2))))
      (let [data (<! data-chan)]
        ;; the format we need to pull out is after a string content of "ea"
        ;; table fmt is "ea", name, type, # ordered, # shipped, id, # U/M, total price, retail price, extended price
        (map #(.-str %) (.-content (first (.-pages data))))))))

(defn main [& cli-args]
  (println (extract (first cli-args))))
