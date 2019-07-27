(ns extract
  (:require-macros [cljs.core.async.macros :refer [go]])
  (:require [cljs.core.async :refer [chan put! <! close!]]
            ["pdf.js-extract" :refer [PDFExtract]]
            ["util" :as util]))

(defn main [& cli-args]
  (go
    (let [data-chan (chan)]
      (.extract (new PDFExtract)
                (first cli-args)
                #js{}
                #(do (if %1
                       (js/console.log %1)
                       (put! data-chan %2))
                     (close! data-chan)))
      (let [data (<! data-chan)]
        (js/console.log (util/inspect data false nil true))))))
