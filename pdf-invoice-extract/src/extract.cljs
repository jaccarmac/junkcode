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
    (go (when-let [data (<! data-chan)]
          (->> (.-content (first (.-pages data)))
               (sort-by #(vector (.-y %) (.-x %)))
               (map #(.-str %)))))))

(defn main [& cli-args]
  (go (println (<! (extract (first cli-args))))))
