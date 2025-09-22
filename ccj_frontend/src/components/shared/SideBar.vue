<template>
	<v-navigation-drawer expand-on-hover permanent rail>
		<v-list>
			<v-list-item prepend-avatar="https://randomuser.me/api/portraits/women/90.jpg" subtitle="sandra_a88@gmailcom"
				title="Sandra Adams"></v-list-item>
		</v-list>

		<v-divider></v-divider>

		<v-list density="compact" nav>
			<v-list-item color="primary" prepend-icon="mdi-domain" title="Объекты" value="buildingObjects"></v-list-item>
			<v-list-item color="primary" prepend-icon="mdi-cog" title="Настройки" value="options"></v-list-item>
			<v-spacer></v-spacer>
			<v-list-item color="primary" prepend-icon="mdi-logout" title="Выйти" value="logout"></v-list-item>
		</v-list>
	</v-navigation-drawer>
</template>
