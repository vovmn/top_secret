<template>
	<v-card :class="containerClass" :title="title" :color="color" style="height: 100%">
		<v-card-text>
			<slot>{{ text }}</slot>
		</v-card-text>
		<v-card-actions>
			<v-btn size="small" class="font-weight-bold" variant="text" :to="link">
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
	containerClass: { type: String, default: '' }
})
</script>