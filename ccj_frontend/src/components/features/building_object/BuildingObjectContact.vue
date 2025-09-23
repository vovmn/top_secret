<template>
	<v-card class="mx-auto" :subtitle="subtitle" :title="title">
		<template #prepend>
			<v-avatar color="blue-darken-2">
				<v-img :alt="title" :src="src"></v-img>
			</v-avatar>
		</template>
		<v-card-text>
			<v-btn class="mr-3" size="small" variant="outlined" color="success" dark>Позвонить</v-btn>
			<v-btn size="small" variant="outlined" color="accent" dark>Написать в чат</v-btn>
		</v-card-text>
	</v-card>
</template>

<script setup lang="ts">
	defineProps<{
		title: string
		subtitle: string
		src: string
	}>();
</script>

<style scoped lang="sass">

</style>

