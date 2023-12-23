import { transactionsImportSchema } from '$lib/schemas/transactions-import';
import { fail } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import { superValidate } from 'sveltekit-superforms/server';
import { parseString } from '@fast-csv/parse';

export const load = (async () => {
	const form = await superValidate(transactionsImportSchema);
	return { form };
}) satisfies PageServerLoad;

export const actions = {
	default: async ({ request }) => {
		const formData = await request.formData();
		const form = await superValidate(formData, transactionsImportSchema);

		if (!form.valid) return fail(400, { form });

		console.log(formData);

		const file = formData.get('importFile');
		if (file instanceof File) {
			const csv = await file.text();
			parseString<>(csv, { headers: true, skipLines: 13 }).on('data', (row) => console.log(row));
		}

		return { form };
	}
};
