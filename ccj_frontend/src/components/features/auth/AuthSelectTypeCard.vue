<template>
  <v-hover>
    <template v-slot:default="{ isHovering, props }">
      <v-card :to="to" class="cursor-pointer" :title="title" v-bind="props" :color="isHovering ? 'info' : 'primary'">
        <v-card-text>
          <v-img :src="src"></v-img>
        </v-card-text>
      </v-card>
    </template>
  </v-hover>
</template>

<script setup lang="ts">
defineProps<{
  title: string
  src: string
  to: string
}>();
</script>

<style scoped lang="sass">

</style>
