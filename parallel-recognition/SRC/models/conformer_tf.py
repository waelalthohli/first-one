import tensorflow as tf

def build_transformer_speech(input_shape=(16000,), num_classes=35, d_model=128, num_heads=4, ff_dim=256):
    inputs = tf.keras.Input(shape=input_shape)
    x = tf.expand_dims(inputs, -1)
    # تحويل waveform إلى spectrogram بسيط
    x = tf.signal.stft(tf.squeeze(x, -1), frame_length=256, frame_step=128)
    x = tf.abs(x)
    x = tf.keras.layers.Resizing(128, d_model)(x)  # project to d_model
    # apply a few Transformer encoder layers
    for _ in range(3):
        attn = tf.keras.layers.MultiHeadAttention(num_heads=num_heads, key_dim=d_model)(x, x)
        x = tf.keras.layers.LayerNormalization()(x + attn)
        ff = tf.keras.layers.Dense(ff_dim, activation='relu')(x)
        ff = tf.keras.layers.Dense(d_model)(ff)
        x = tf.keras.layers.LayerNormalization()(x + ff)
    x = tf.keras.layers.GlobalAveragePooling1D()(x)
    outputs = tf.keras.layers.Dense(num_classes, activation='softmax')(x)
    model = tf.keras.Model(inputs=inputs, outputs=outputs)
    return model
