<script lang="ts">
	import type { PageData } from './$types';
	import { superForm } from 'sveltekit-superforms/client';

	export let data: PageData;

	const { form, errors, constraints, message, enhance } = superForm(data.form, {
		taintedMessage: 'Seite wirklich verlassen? Eventuelle Änderungen werden nicht gespeichert!'
	});
</script>

<h1>Umsätze importieren</h1>

{#if $message}
	<div class="alert alert-error">{$message}</div>
{/if}

<form
	method="POST"
	class="form-control flex w-full max-w-none flex-col gap-4 lg:max-w-md"
	use:enhance
>
	<div>
		<label class="label pb-1 text-sm" for="description">Beschreibung</label>
		<input
			type="file"
			name="importFile"
			class="input"
			accept=".csv"
			class:input-error={$errors.importFile}
			aria-invalid={$errors.importFile ? 'true' : undefined}
			required
		/>
		{#if $errors.importFile}
			<label for="importFile" class="label p-0 pt-1">
				<span class="label-text-alt ml-4 text-error">{$errors.importFile}</span>
			</label>
		{/if}
	</div>

	<div class="flex">
		<button type="submit" class="min-w-150 btn btn-primary mr-5">Ok</button>
	</div>
</form>
