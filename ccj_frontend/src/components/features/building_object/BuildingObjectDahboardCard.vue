<template>
  <v-card :class="containerClass" :color="color" style="height: 100%" :title="title">
    <v-card-text>
      <slot>{{ text }}</slot>
    </v-card-text>
    <v-card-actions>
      <v-btn class="font-weight-bold" size="small" :to="link" variant="text">
        {{ actionText }}
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script setup>
  defineProps({
    title: { type: String, required: true },
    text: { type: String, default: '' },
    color: { type: String, default: 'info' },
    link: { type: String, required: true },
    actionText: { type: String, default: 'Просмотреть' },
    containerClass: { type: String, default: '' },
  })
</script>
