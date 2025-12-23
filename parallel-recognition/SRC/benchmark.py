import time
import numpy as np
import tensorflow as tf

def measure_inference_latency(model, input_sample, runs=200):
    # input_sample: np array batched shape [B,...]
    # warmup
    for _ in range(10):
        _ = model.predict(input_sample)
    times = []
    for _ in range(runs):
        t0 = time.time()
        _ = model.predict(input_sample)
        t1 = time.time()
        times.append(t1 - t0)
    lat_ms = (np.mean(times) / input_sample.shape[0]) * 1000
    throughput = input_sample.shape[0] / np.mean(times)
    return lat_ms, throughput
