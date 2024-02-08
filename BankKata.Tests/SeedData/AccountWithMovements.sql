INSERT INTO public."Accounts" ("Id", "Balance") VALUES ('881b2297-1c8f-4ef2-b80c-bfa5a43107ae', 0);
INSERT INTO public."Accounts" ("Id", "Balance") VALUES ('2d61906c-d856-4b3b-89b1-67673ee5499c', 500);

INSERT INTO public."Movements" ("Id", "AccountId", "Amount", "Balance") VALUES ('c19c9456-7ed9-4cc4-8e57-b4eb46fb5bf4', '881b2297-1c8f-4ef2-b80c-bfa5a43107ae', 0, 0);
INSERT INTO public."Movements" ("Id", "AccountId", "Amount", "Balance") VALUES ('dbbd6c7f-87b6-4a74-bb9e-7fa24ca2c118', '2d61906c-d856-4b3b-89b1-67673ee5499c', 500, 500);