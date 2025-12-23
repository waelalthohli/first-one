import tensorflow as tf
from data_speech import get_speech_dataset
from models.conformer_tf import build_transformer_speech

def setup_tpu():
    try:
        resolver = tf.distribute.cluster_resolver.TPUClusterResolver()
        tf.config.experimental_connect_to_cluster(resolver)
        tf.tpu.experimental.initialize_tpu_system(resolver)
        strategy = tf.distribute.TPUStrategy(resolver)
        print("Running on TPU")
    except Exception as e:
        print("TPU not found, using default strategy:", e)
        strategy = tf.distribute.get_strategy()
    return strategy

def main(batch_size=128, epochs=20):
    strategy = setup_tpu()
    with strategy.scope():
        model = build_transformer_speech(input_shape=(16000,), num_classes=35)
        model.compile(optimizer=tf.keras.optimizers.Adam(1e-3),
                      loss='sparse_categorical_crossentropy',
                      metrics=['accuracy'])
    train_ds, test_ds = get_speech_dataset(name='speech_commands', batch_size=batch_size, duration=1.0)
    model.fit(train_ds, epochs=epochs, validation_data=test_ds)
    model.save('speech_transformer_tpu.h5')


if __name__ == "__main__":
    import argparse
    parser = argparse.ArgumentParser(description='Train speech transformer on Speech Commands')
    parser.add_argument('--batch_size', type=int, default=128)
    parser.add_argument('--epochs', type=int, default=20)
    args = parser.parse_args()
    main(batch_size=args.batch_size, epochs=args.epochs)
