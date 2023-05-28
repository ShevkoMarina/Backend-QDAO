------------------Таблица-пользователей-----------------
create table if not exists users (
	id				serial					primary key,
	account			varchar(42)				not null,
	role			smallint				not null,
	login			text					not null,
	password		text					not null
);

create unique index if not exists idx_uq_login on users (login);

insert into users (account, role, login, password) values ('', 3, 'admin', 'admin');


-------------------Таблица-предложений-------------------

create table if not exists proposal (
	id				bigint						primary key,
	proposer_id		int	references users (id)	default 0,
	name			text						not null,
	description		text						not null,
	created_at		timestamp without time zone not null default (now() at time zone 'utc')
);

-------------------Таблица-голосования-------------------

create table if not exists voting (
	proposal_id		bigint references proposal (id) default 0 primary key,
	start_block		bigint							not null,
	end_block		bigint							not null,
	votes_for		int								default 0,
	votes_against	int								default 0,
	eta				bigint			
);


-------------------Таблица-статусов-предложений-----------

create table if not exists proposal_state_log (
	proposal_id	bigint	references proposal (id) default 0,
	state		smallint						not null,
	created_at	timestamp without time zone		not null default (now() at time zone 'utc')
);

create index if not exists idx_proposal_state_log on proposal_state_log 
using btree(proposal_id, created_at) include (state);


--------------------Очередь-обработки-транзакций-----------

create table if not exists transaction_queue (
	id				bigserial	primary key,
	hash			text		not null,
	state			smallint	default 0,
	created_at	timestamp without time zone not null default (now() at time zone 'utc'),
	processed_at	timestamp	without time zone
);