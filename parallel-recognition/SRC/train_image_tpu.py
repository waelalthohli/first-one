import tensorflow as tf
from data_image import get_image_dataset
from models.resnet_tf import build_resnet

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

def main(batch_size=512, epochs=10):
    strategy = setup_tpu()
    with strategy.scope():
        model = build_resnet(input_shape=(32,32,3), num_classes=10)
        model.compile(optimizer=tf.keras.optimizers.Adam(1e-3),
                      loss='sparse_categorical_crossentropy',
                      metrics=['accuracy'])
    train_ds, test_ds = get_image_dataset(batch_size=batch_size, img_size=32)
    model.fit(train_ds, epochs=epochs, validation_data=test_ds)
    model.save('resnet_cifar10_tpu.h5')


if __name__ == "__main__":
    import argparse
    parser = argparse.ArgumentParser(description='Train ResNet on CIFAR-10 (TPU-friendly)')
    parser.add_argument('--batch_size', type=int, default=512)
    parser.add_argument('--epochs', type=int, default=10)
    args = parser.parse_args()
    main(batch_size=args.batch_size, epochs=args.epochs)
